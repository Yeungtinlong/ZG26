using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public static class AnimationClipGeneratorLogic
    {
        public static void GenerateClipsBySpriteAtlas(IList<SpriteAtlas> spriteAtlasList, int frameRate)
        {
            foreach (var spriteAtlas in spriteAtlasList)
            {
                Object[] packables = spriteAtlas.GetPackables();
                if (packables == null || packables.Length == 0)
                {
                    Debug.LogError($"Objects for Packing list is empty in {spriteAtlas.name}.");
                    continue;
                }

                List<Sprite> spriteList = null;
                string assetPath = AssetDatabase.GetAssetPath(packables[0]);

                if (AssetDatabase.IsValidFolder(assetPath) && packables.Length == 1)
                    spriteList = AssetDatabaseUtils.GetAllAssetsOfType<Sprite>(assetPath);
                else
                    spriteList = packables.Cast<Sprite>().ToList();

                string savePath = AssetDatabase.GetAssetPath(spriteAtlas);
                savePath = savePath.Substring(0, savePath.LastIndexOf('/'));
                string newAssetPath = $"{savePath}/{spriteAtlas.name}.anim";

                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(newAssetPath);
                bool newClip = false;
                if (clip == null)
                {
                    clip = new AnimationClip();
                    newClip = true;
                }

                clip.name = spriteAtlas.name;
                clip.frameRate = frameRate;

                var curveBinding = new EditorCurveBinding
                {
                    type = typeof(SpriteRenderer),
                    path = "",
                    propertyName = "m_Sprite"
                };
                var keyframes = new ObjectReferenceKeyframe[spriteList.Count];

                float interval = 1f / frameRate;

                for (int i = 0; i < keyframes.Length; i++)
                {
                    keyframes[i].time = i * interval;
                    keyframes[i].value = spriteList[i];
                }

                AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyframes);

                // if (AssetDatabase.LoadAssetAtPath<AnimationClip>(newAssetPath) != null)
                //     AssetDatabase.DeleteAsset(newAssetPath);

                if (newClip)
                {
                    AssetDatabase.CreateAsset(clip, newAssetPath);
                }
                else
                {
                    EditorUtility.SetDirty(clip);
                }

                AssetDatabase.SaveAssets();
            }
        }

        public static List<string> ScanTexture2DPathsByFolder(Object folder)
        {
            if (folder == null)
            {
                return null;
            }

            string relativePath = AssetDatabase.GetAssetPath(folder);
            if (!AssetDatabase.IsValidFolder(relativePath))
            {
                return null;
            }

            HashSet<string> paths = new HashSet<string>();

            AssetDatabaseUtils.RecursionGroupFolderPathOfType<Texture2D>(relativePath, paths);

            return paths.ToList();
        }

        public static List<SpriteAtlas> ScanSpriteAtlasByFolders(List<Object> folders)
        {
            if (folders == null || folders.Count == 0)
                return null;

            var spriteAtlasList = new List<SpriteAtlas>();
            foreach (var folder in folders)
            {
                string relativePath = AssetDatabase.GetAssetPath(folder);
                if (!AssetDatabase.IsValidFolder(relativePath))
                    continue;

                var sas = AssetDatabaseUtils.GetAllAssetsOfType<SpriteAtlas>(relativePath);
                spriteAtlasList.AddRange(sas);
            }

            return spriteAtlasList;
        }

        public static List<string> ScanTexture2DPathsByFolders(List<Object> folders)
        {
            if (folders == null || folders.Count == 0)
                return null;

            HashSet<string> paths = new HashSet<string>();

            foreach (var folder in folders)
            {
                string relativePath = AssetDatabase.GetAssetPath(folder);
                if (!AssetDatabase.IsValidFolder(relativePath))
                    continue;

                AssetDatabaseUtils.RecursionGroupFolderPathOfType<Texture2D>(relativePath, paths);
            }

            return paths.ToList();
        }

        public static List<SpriteAtlas> GenerateSpriteAtlasList(List<string> texture2DFolderPaths,
            GenerateSpriteAtlasSettings settings)
        {
            List<string> createdSpriteAtlasPathList = new List<string>();

            foreach (var path in texture2DFolderPaths)
            {
                var guids = AssetDatabase.FindAssets("t:Texture2D", new[] { path });

                List<Texture2D> texture2Ds = new List<Texture2D>();

                foreach (var guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                    Texture2D texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

                    if (AssetImporter.GetAtPath(assetPath) is not TextureImporter importer)
                        continue;

                    // Platform
                    var textureImporterPlatformSettings = importer.GetPlatformTextureSettings(settings.Platform);
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spriteImportMode = SpriteImportMode.Single;

                    if (settings.IsChangeTexture2DSize)
                        textureImporterPlatformSettings.maxTextureSize = settings.TextureSize;
                    textureImporterPlatformSettings.format = TextureImporterFormat.RGBA32;
                    textureImporterPlatformSettings.textureCompression = TextureImporterCompression.Uncompressed;
                    textureImporterPlatformSettings.overridden = true;

                    importer.SetPlatformTextureSettings(textureImporterPlatformSettings);
                    importer.SaveAndReimport();

                    texture2Ds.Add(texture2D);
                }

                {
                    // SpriteAtlas spriteAtlas = new SpriteAtlas();
                    // spriteAtlas.SetPackingSettings(spriteAtlasPackingSettings);

                    var spriteAtlasAsset = new SpriteAtlasAsset();
                    spriteAtlasAsset.Add(new[] { AssetDatabase.LoadAssetAtPath<Object>(path) });

                    string folderName = path.Substring(path.LastIndexOf('/') + 1);

                    string folderPath = path.Substring(0, path.LastIndexOf('/'));

                    spriteAtlasAsset.name = folderName;

                    string newAssetPath = $"{folderPath}/{spriteAtlasAsset.name}.spriteatlasv2";
                    SpriteAtlasAsset.Save(spriteAtlasAsset, newAssetPath);
// TODO: 等待导入
                    createdSpriteAtlasPathList.Add(newAssetPath);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            foreach (var createdSpriteAtlasPath in createdSpriteAtlasPathList)
            {
                SpriteAtlasPackingSettings spriteAtlasPackingSettings = new SpriteAtlasPackingSettings
                {
                    blockOffset = 1,
                    padding = 2,
                    enableRotation = false,
                    enableTightPacking = false,
                };

                var spriteAtlasImporter = (SpriteAtlasImporter)AssetImporter.GetAtPath(createdSpriteAtlasPath);
                spriteAtlasImporter.packingSettings = spriteAtlasPackingSettings;

                var textureImporterPlatformSettings = spriteAtlasImporter.GetPlatformSettings(settings.Platform);
                textureImporterPlatformSettings.maxTextureSize = settings.SpriteAtlasSize;
                textureImporterPlatformSettings.format = settings.SpriteAtlasFormat;
                textureImporterPlatformSettings.overridden = true;

                spriteAtlasImporter.SetPlatformSettings(textureImporterPlatformSettings);
                spriteAtlasImporter.SaveAndReimport();
            }

            // Enum.TryParse<BuildTarget>(settings.Platform, out var targetPlatform);
            // SpriteAtlasUtility.PackAtlases(spriteAtlasList.ToArray(), targetPlatform);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return null;
        }

        public static void ChangeSpriteAtlasSettings(List<SpriteAtlas> spriteAtlasList,
            GenerateSpriteAtlasSettings settings)
        {
            foreach (var createdSpriteAtlas in spriteAtlasList)
            {
                string createdSpriteAtlasPath = AssetDatabase.GetAssetPath(createdSpriteAtlas);

                var spriteAtlasPackingSettings = new SpriteAtlasPackingSettings
                {
                    blockOffset = 1,
                    padding = 2,
                    enableRotation = false,
                    enableTightPacking = false,
                };

                var spriteAtlasImporter = (SpriteAtlasImporter)AssetImporter.GetAtPath(createdSpriteAtlasPath);
                spriteAtlasImporter.packingSettings = spriteAtlasPackingSettings;

                var textureImporterPlatformSettings = spriteAtlasImporter.GetPlatformSettings(settings.Platform);
                textureImporterPlatformSettings.maxTextureSize = settings.SpriteAtlasSize;
                textureImporterPlatformSettings.format = settings.SpriteAtlasFormat;
                textureImporterPlatformSettings.overridden = true;

                spriteAtlasImporter.SetPlatformSettings(textureImporterPlatformSettings);
                spriteAtlasImporter.SaveAndReimport();
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void SetSpriteAtlasPivotAsFirstSprite(List<Object> spritesContainerFolders)
        {
            foreach (var folder in spritesContainerFolders)
            {
                string folderPath = AssetDatabase.GetAssetPath(folder);
                if (!AssetDatabase.IsValidFolder(folderPath))
                {
                    continue;
                }

                string rootPath = AssetDatabase.GetAssetPath(folder);
                HashSet<string> paths = new HashSet<string>();
                AssetDatabaseUtils.RecursionGroupFolderPathOfType<Texture2D>(rootPath, paths);

                foreach (var path in paths)
                {
                    var texture2dList = AssetDatabaseUtils.GetAllAssetsOfType<Texture2D>(path);
                    texture2dList = texture2dList.OrderBy(t => t.name).ToList();

                    Vector2 pivot =
                        (AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture2dList[0])) as TextureImporter)
                        .spritePivot;

                    for (int i = 1; i < texture2dList.Count; i++)
                    {
                        string texture2dPath = AssetDatabase.GetAssetPath(texture2dList[i]);
                        var importer = AssetImporter.GetAtPath(texture2dPath) as TextureImporter;

                        TextureImporterSettings settings = new TextureImporterSettings();
                        importer.ReadTextureSettings(settings);

                        settings.spritePivot = pivot;
                        settings.spriteAlignment = (int)SpriteAlignment.Custom;

                        importer.SetTextureSettings(settings);
                        // importer.SaveAndReimport();
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 为某个代表一个角色的目录创建OverrideAnimationController和PrefabVariant，并且自动赋值，由此产生最终的角色View预制体
        /// </summary>
        /// <param name="folderObjects">代表一个角色的文件夹</param>
        /// <param name="virtualController"></param>
        /// <param name="virtualViewPrefab"></param>
        public static void GenerateOverrideAnimationControllerAndPrefabVariant(
            List<Object> folderObjects,
            RuntimeAnimatorController virtualController,
            GameObject virtualViewPrefab,
            GameObject virtualLogicPrefab, string logicPrefabVariantTargetPath
        )
        {
            if (folderObjects is not { Count: > 0 })
                return;

            foreach (var folderObject in folderObjects)
            {
                string folderPath = AssetDatabase.GetAssetPath(folderObject);

                List<AnimationClip> clips = AssetDatabaseUtils.GetAllAssetsOfType<AnimationClip>(folderPath);

                var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                AnimatorOverrideController newOverrideController = new AnimatorOverrideController();
                newOverrideController.runtimeAnimatorController = virtualController;
                newOverrideController.GetOverrides(overrides);

                var overrideDict = new Dictionary<AnimationClip, AnimationClip>();

                foreach (var kvp in overrides)
                {
                    AnimationClip virtualClip = kvp.Key;
                    AnimationClip overrideClip = clips.FirstOrDefault(c => c.name == virtualClip.name);
                    if (overrideClip == null)
                    {
                        // 匹配方向名，如 attack_left 匹配不到，就用 move_left 来重写 attack_left
                        string[] dirNameSplits = virtualClip.name.Split('_');
                        if (dirNameSplits is { Length: 2 })
                        {
                            overrideClip = clips.FirstOrDefault(c =>
                            {
                                string[] clipNameSplits = c.name.Split('_');
                                if (clipNameSplits is { Length: 2 })
                                    return clipNameSplits[1] == dirNameSplits[1];
                                return false;
                            });
                            if (overrideClip == null)
                            {
                                Debug.LogWarning($"Cannot find overrideClip for {virtualClip.name} in {folderPath}.");
                                continue;
                            }
                        }
                    }

                    overrideDict.Add(virtualClip, overrideClip);
                }

                newOverrideController.ApplyOverrides(overrideDict.ToList());

                string folderName = folderPath.Substring(folderPath.LastIndexOf('/') + 1);
                newOverrideController.name = $"{folderName}_AnimatorOverrideController";
                string newAssetPath = $"{folderPath}/{newOverrideController.name}.overrideController";
                AssetDatabase.CreateAsset(newOverrideController, newAssetPath);

                // 产生美术预制体变体，并保存在Arts目录下
                string overrideViewPrefabPath = $"{folderPath}/{folderName}_CharacterView.prefab";
                {
                    // 实例化预制体到当前Active场景中
                    GameObject instanceRoot = (GameObject)PrefabUtility.InstantiatePrefab(virtualViewPrefab);
                    // 定义AnimationClip中某一行，指定：路径->组件类型->字段
                    EditorCurveBinding curveBinding = new EditorCurveBinding()
                    {
                        path = "",
                        type = typeof(SpriteRenderer),
                        propertyName = "m_Sprite",
                    };
                    ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clips[0], curveBinding);
                    instanceRoot.GetComponent<SpriteRenderer>().sprite = (Sprite)keyframes[0].value;
                    instanceRoot.GetComponent<Animator>().runtimeAnimatorController = newOverrideController;

                    // 自定义预制体变体覆盖预制体的属性（）
                    // PrefabUtility.SetPropertyModifications(instanceRoot, new PropertyModification[] { new PropertyModification() {} });

                    // 保存为预制体
                    PrefabUtility.SaveAsPrefabAsset(instanceRoot, overrideViewPrefabPath);
                    Object.DestroyImmediate(instanceRoot);
                }

                // 产生逻辑预制体变体，并保存在资源加载目录下
                {
                    GameObject instanceRoot = (GameObject)PrefabUtility.InstantiatePrefab(virtualLogicPrefab);
                    Transform viewT = instanceRoot.transform.Find("View");
                    GameObject overrideViewPrefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(overrideViewPrefabPath), viewT);

                    PrefabUtility.SaveAsPrefabAsset(instanceRoot, $"{logicPrefabVariantTargetPath}/{folderName}.prefab");
                    Object.DestroyImmediate(instanceRoot);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spritesContainerFolders"></param>
        /// <param name="isChangeBrotherFolders">比如角色有idle、attack、die、move状态动画的sprite，并且这些目录处于同级，则idle、die、move的pivot也向attack看齐（因为"attack" ASCII小排更前）</param>
        public static void SyncSpritePivots(List<Object> spritesContainerFolders, bool isChangeBrotherFolders)
        {
            try
            {
                AssetDatabase.StartAssetEditing();

                foreach (var topFolderObject in spritesContainerFolders)
                {
                    // topFolderPath: 通常是角色目录
                    string topFolderPath = AssetDatabase.GetAssetPath(topFolderObject);
                    if (!AssetDatabase.IsValidFolder(topFolderPath))
                    {
                        continue;
                    }

                    // subPath: 通常是动作目录
                    HashSet<string> subPaths = new HashSet<string>();
                    AssetDatabaseUtils.RecursionGroupFolderPathOfType<Texture2D>(topFolderPath, subPaths);

                    if (isChangeBrotherFolders)
                    {
                        List<string> brotherFolders = subPaths.ToList();
                        brotherFolders.Sort();
                        // 以同级目录第0个目录的第0个Sprite Pivot为准
                        var texture2dList = AssetDatabaseUtils.GetAllAssetsOfType<Texture2D>(brotherFolders[0]);
                        texture2dList = texture2dList.OrderBy(t => t.name).ToList();
                        Vector2 pivot = (AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture2dList[0])) as TextureImporter).spritePivot;
                        foreach (var brotherFolder in brotherFolders)
                        {
                            SyncPivots(brotherFolder, pivot);
                        }
                    }
                    else
                    {
                        foreach (var subPath in subPaths)
                        {
                            var texture2dList = AssetDatabaseUtils.GetAllAssetsOfType<Texture2D>(subPath);
                            texture2dList = texture2dList.OrderBy(t => t.name).ToList();
                            // 选择第一张 Texture2D 作为参考 pivot
                            Vector2 pivot = (AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(texture2dList[0])) as TextureImporter).spritePivot;

                            SyncPivots(subPath, pivot);
                        }
                    }
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
            }
        }

        /// <summary>
        /// 将目录下所有Sprite的Pivot设为和首个一致
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="pivot"></param>
        public static void SyncPivots(string folderPath, Vector2 pivot)
        {
            var texture2dList = AssetDatabaseUtils.GetAllAssetsOfType<Texture2D>(folderPath);
            for (int i = 0; i < texture2dList.Count; i++)
            {
                string texture2dPath = AssetDatabase.GetAssetPath(texture2dList[i]);
                var importer = AssetImporter.GetAtPath(texture2dPath) as TextureImporter;

                TextureImporterSettings settings = new TextureImporterSettings();
                importer.ReadTextureSettings(settings);

                settings.spritePivot = pivot;
                settings.spriteAlignment = (int)SpriteAlignment.Custom;

                importer.SetTextureSettings(settings);
                importer.SaveAndReimport();
            }
        }
    }
}