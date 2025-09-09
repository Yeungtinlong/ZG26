using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public static class AssetDatabaseUtils
    {
        public static List<T> GetAllAssetsOfType<T>() where T : Object
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { "Assets" });
            return GetAllAssetsByGuids<T>(guids);
        }

        public static List<T> GetAllAssetsOfType<T>(string assetDirectory) where T : Object
        {
            if (!assetDirectory.StartsWith("Assets"))
            {
                Debug.LogError("The directory is not starts with \"Assets\"");
                return null;
            }

            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { assetDirectory });
            return GetAllAssetsByGuids<T>(guids);
        }

        public static T GetAssetByName<T>(string assetDirectory, string assetName) where T : Object
        {
            if (!assetDirectory.StartsWith("Assets"))
            {
                Debug.LogError("The directory is not starts with \"Assets\"");
                return null;
            }

            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { assetDirectory });
            var assets = GetAllAssetsByGuids<T>(guids);
            return assets.FirstOrDefault(asset => asset.name == assetName);
        }

        private static List<T> GetAllAssetsByGuids<T>(string[] guids) where T : Object
        {
            List<T> objects = new List<T>();
            foreach (var guid in guids)
            {
                T obj = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
                objects.Add(obj);
            }

            return objects;
        }

        public static void ForEachPrefabRoot(GameObject gameObject, Action<GameObject> operation, bool isSaveBack = true)
        {
            string prefabPath = AssetDatabase.GetAssetPath(gameObject);
            bool isAsset = AssetDatabase.Contains(gameObject);

            if (isAsset && !PrefabUtility.IsPartOfRegularPrefab(gameObject))
            {
                return;
            }

            GameObject instance = !isAsset ? gameObject : PrefabUtility.LoadPrefabContents(prefabPath);

            operation?.Invoke(instance);

            if (isAsset)
            {
                if (isSaveBack)
                {
                    if (EditorUtility.IsDirty(instance))
                    {
                        PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                    }
                }

                PrefabUtility.UnloadPrefabContents(instance);
            }
        }

        public static void ForEachSelectedAssetRoot(Action<GameObject> operation, bool isSaveBack = true)
        {
            GameObject[] gameObjects = Selection.gameObjects;
            foreach (var go in gameObjects)
            {
                string prefabPath = AssetDatabase.GetAssetPath(go);
                bool isSelectingInstance = string.IsNullOrEmpty(prefabPath);
                GameObject instance = isSelectingInstance ? go : PrefabUtility.LoadPrefabContents(prefabPath);

                operation?.Invoke(instance);

                if (!isSelectingInstance)
                {
                    if (isSaveBack)
                    {
                        PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                    }

                    PrefabUtility.UnloadPrefabContents(instance);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void OperateAllFilesFromDirectory(string path, bool recursive, Action<FileInfo> operation)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            if (recursive)
                foreach (var subDirectory in directory.GetDirectories())
                    OperateAllFilesFromDirectory(subDirectory.FullName, recursive, operation);

            foreach (var file in directory.GetFiles())
                operation.Invoke(file);
        }

        public static void ForEachAllGameObjectPrefabAssetsWithChildrenNodes(Predicate<GameObject> selector,
            Action<GameObject> action)
        {
            List<GameObject> allPrefabs = GetAllAssetsOfType<GameObject>();
            allPrefabs.ForEach(prefab =>
            {
                ForEachPrefabRoot(prefab, rootGameObject =>
                {
                    List<Transform> transforms =
                        Utils.FindAllTBFS(rootGameObject.transform, t => selector.Invoke(t.gameObject));
                    transforms.ForEach(t => action.Invoke(t.gameObject));
                });
            });
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static string AssetPathToAbsolutePath(string assetPath)
        {
            return
                $"{Application.dataPath}/{(string.IsNullOrEmpty(assetPath) ? string.Empty : assetPath.TrimStart("Assets/".ToCharArray()))}";
        }
        
        public static bool TryChangeAssetGuid(string assetPath, string newGuid, bool force = false)
        {
            string path = AssetDatabase.GUIDToAssetPath(newGuid);
            if (!string.IsNullOrEmpty(path) && !force)
            {
                Debug.LogError($"This guid is used by asset {path}.");
                return false;
            }

            string fullPath = $"{Path.GetFullPath(assetPath)}.meta";
            string metaText = File.ReadAllText(fullPath);

            Regex regex = new Regex(@"(guid: )(-*[\d\w]+)");
            string result = regex.Replace(metaText, @$"guid: {newGuid}");
            
            File.WriteAllText(fullPath, result);

            return true;
        }
        
        /// <summary>
        /// 递归查找所有将 <typeparamref name="T"/> 类型组织起来的目录
        /// </summary>
        /// <param name="currentPath">递归起点</param>
        /// <param name="paths">被找到的所有目录</param>
        /// <typeparam name="T">被目录组织起来的Asset类型</typeparam>
        public static void RecursionGroupFolderPathOfType<T>(string currentPath, HashSet<string> paths) where T : Object
        {
            var guids = AssetDatabase.FindAssets("t:Object", new[] { currentPath });
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);

                Object asset = AssetDatabase.LoadAssetAtPath<Object>(path);

                if (asset is T)
                {
                    paths.Add(Path.GetDirectoryName(path).Replace('\\', '/'));
                }
            }

            string[] subFolders = AssetDatabase.GetSubFolders(currentPath);

            foreach (var subFolder in subFolders)
            {
                RecursionGroupFolderPathOfType<T>(subFolder, paths);
            }
        }
    }
}