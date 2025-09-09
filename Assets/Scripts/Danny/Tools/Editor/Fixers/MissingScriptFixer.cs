using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using SupportUtils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SupportUtils
{
    public class MissingScriptFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/Missing Script Fixer")]
        static void OpenWindow()
        {
            GetWindow<MissingScriptFixer>("MISSING SCRIPTS").Show();
        }
        
        [BoxGroup("Scene", order: 0f)] [SerializeField] private List<GameObject> _missingScriptsObjects;

        [BoxGroup("Scene")]
        [Button("SCAN IN ACTIVE SCENE")]
        void FindMissingScriptObjectsInScene()
        {
            _missingScriptsObjects = new List<GameObject>();

            Scene activeScene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = activeScene.GetRootGameObjects();

            foreach (var rootObject in rootObjects)
            {
                foreach (var obj in GetAllMissingScriptObjects(rootObject))
                {
                    if (!_missingScriptsObjects.Contains(obj))
                    {
                        _missingScriptsObjects.Add(obj);
                    }
                }
            }
        }
        
        [BoxGroup("Scene")]
        [Button("DELETE ALL MISSING COMPONENTS IN SCENE")]
        private void DeleteMissingScriptsInScene()
        {
            List<GameObject> prefabs = _missingScriptsObjects;

            prefabs.ForEach(prefab =>
            {
                AssetDatabaseUtils.ForEachPrefabRoot(prefab, rootGameObject =>
                {
                    List<Transform> transforms =
                        Utils.FindAllTBFS(rootGameObject.transform, t => t != null);

                    transforms.ForEach(t =>
                    {
                        int removedCount = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject);
                        if (removedCount > 0)
                        {
                            Debug.Log($"Removed {removedCount} missing script from {t.gameObject.name}!");
                        }
                    });
                });
            });
        }

        [BoxGroup("Prefab Mode", order: 1f)]
        [Button("SCAN IN PREFAB MODE")]
        void FindMissingScriptsInPrefabMode()
        {
            _missingScriptsObjects = new List<GameObject>();

            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            GameObject root = prefabStage.prefabContentsRoot;

            foreach (var obj in GetAllMissingScriptObjects(root))
            {
                if (!_missingScriptsObjects.Contains(obj))
                {
                    _missingScriptsObjects.Add(obj);
                }
            }
        }

        public static List<GameObject> GetAllMissingScriptObjects(GameObject root)
        {
            return Utils.FindAllTBFS(
                    root.transform,
                    t => t.GetComponents<Component>()
                        .ToList()
                        .Exists(c => c == null)
                )
                .Select(t => t.gameObject)
                .ToList();
        }

        [BoxGroup("Asset Folder", order: 2f)] [SerializeField]
        private List<GameObject> _missingScriptsObjectsInAssetFolder;

        [BoxGroup("Asset Folder")]
        [Button("SCAN IN ASSET FOLDER")]
        void FindMissingScriptsInAssets()
        {
            _missingScriptsObjectsInAssetFolder = new List<GameObject>();

            List<GameObject> gameObjects = AssetDatabaseUtils.GetAllAssetsOfType<GameObject>();
            foreach (var gameObject in gameObjects)
            {
                AssetDatabaseUtils.ForEachPrefabRoot(gameObject, go =>
                {
                    List<Transform> missingScriptTrans = Utils.FindAllTBFS(
                        go.transform,
                        t => GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject) > 0
                    );

                    if (missingScriptTrans.Count > 0)
                    {
                        _missingScriptsObjectsInAssetFolder.Add(gameObject);
                    }

                    // List<Component> components = go.GetComponents<Component>().ToList();
                    //
                    // if (components.Exists(c => c == null))
                    // {
                    //     _missingScriptsObjects.Add(gameObject);
                    // }
                }, false);
            }
        }
        
        [BoxGroup("Asset Folder")]
        [Button("DELETE ALL MISSING COMPONENTS")]
        private void DeleteMissingScripts()
        {
            List<GameObject> prefabs = _missingScriptsObjectsInAssetFolder;

            prefabs.ForEach(prefab =>
            {
                AssetDatabaseUtils.ForEachPrefabRoot(prefab, rootGameObject =>
                {
                    List<Transform> transforms =
                        Utils.FindAllTBFS(rootGameObject.transform, t => t != null);

                    transforms.ForEach(t =>
                    {
                        int removedCount = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject);
                        if (removedCount > 0)
                        {
                            Debug.Log($"Removed {removedCount} missing script from {t.gameObject.name}!");
                        }
                    });
                });
            });
        }
    }
}