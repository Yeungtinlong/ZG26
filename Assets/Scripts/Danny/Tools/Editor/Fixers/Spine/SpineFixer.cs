// #define DANNY_SPINE_SUPPORT
#if DANNY_SPINE_SUPPORT

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class SpineFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/Spine Fixer")]
        static void OpenWindow()
        {
            GetWindow<SpineFixer>("SPINE UTILITIES").Show();
        }

        [TabGroup("SPINE", "ALPHA FIX")] [SerializeField]
        [InfoBox("修复Spine资源引用的Texture选中了Alpha is Transparency")]
        private List<Texture2D> _textures;

        [TabGroup("SPINE", "ALPHA FIX")] [SerializeField]
        private bool _alphaIsTransparency;

        [TabGroup("SPINE", "ALPHA FIX")]
        [Button("Scan Spine Sprites")]
        void ScanAllSpineAtlasSprites()
        {
            if (_textures != null)
            {
                _textures.Clear();
            }
            else
            {
                _textures = new List<Texture2D>();
            }

            GetAllMatchedSprites();
        }

        void GetAllMatchedSprites()
        {
            List<SpineAtlasAsset> atlasAssets = AssetDatabaseUtils.GetAllAssetsOfType<SpineAtlasAsset>();
            foreach (var atlasAsset in atlasAssets)
            {
                if (atlasAsset.materials == null)
                {
                    continue;
                }

                foreach (var atlasAssetMaterial in atlasAsset.materials)
                {
                    if (atlasAssetMaterial.mainTexture == null)
                    {
                        continue;
                    }

                    string path = AssetDatabase.GetAssetPath(atlasAssetMaterial.mainTexture);

                    if (AssetImporter.GetAtPath(path) is not TextureImporter textureImporter)
                    {
                        continue;
                    }

                    if (textureImporter.alphaIsTransparency == _alphaIsTransparency)
                    {
                        _textures.Add(atlasAssetMaterial.mainTexture as Texture2D);
                    }
                }
            }
        }

        [TabGroup("SPINE", "ALPHA FIX")]
        [Button("Fix Spine Sprites")]
        private void FixAllSpineSprites()
        {
            foreach (var texture in _textures)
            {
                string path = AssetDatabase.GetAssetPath(texture);

                if (AssetImporter.GetAtPath(path) is not TextureImporter textureImporter)
                {
                    continue;
                }

                textureImporter.alphaIsTransparency = false;
                textureImporter.SaveAndReimport();
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [TabGroup("SPINE", "EXPORT PROJ")] [SerializeField]
        [InfoBox("导出所有SkeletonDataAsset对应的Spine项目文件")]
        private string _exportPath;

        [TabGroup("SPINE", "EXPORT PROJ")] [SerializeField]
        private List<SkeletonDataAsset> _dataAssets;

        [TabGroup("SPINE", "EXPORT PROJ")]
        [Button("EXPORT")]
        void Export()
        {
            if (string.IsNullOrEmpty(_exportPath))
            {
                return;
            }

            var uniqueNameMap = new Dictionary<string, int>();

            foreach (var dataAsset in _dataAssets)
            {
                string outputPath = $"{Path.Combine(_exportPath, dataAsset.name)}";
                uniqueNameMap.TryAdd(outputPath, -1);
                outputPath = $"{outputPath}_{++uniqueNameMap[outputPath]}";

                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                TextAsset skeletonJson = dataAsset.skeletonJSON;
                List<TextAsset> atlasFiles = dataAsset.atlasAssets.Select(a => ((SpineAtlasAsset)a).atlasFile).ToList();
                List<Texture> materials = dataAsset.atlasAssets
                    .SelectMany(a => a.Materials.Select(m => m.mainTexture).ToList()).ToList();

                string jsonPath = Path.GetFullPath(AssetDatabase.GetAssetPath(skeletonJson));
                File.Copy(jsonPath,
                    Path.Combine(outputPath,
                        $"{Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(skeletonJson))}.json"));
                atlasFiles.ForEach(atlas =>
                {
                    string path = Path.GetFullPath(AssetDatabase.GetAssetPath(atlas));
                    File.Copy(path, Path.Combine(outputPath, Path.GetFileName(AssetDatabase.GetAssetPath(atlas))));
                });
                materials.ForEach(mat =>
                {
                    string path = Path.GetFullPath(AssetDatabase.GetAssetPath(mat));
                    File.Copy(path, Path.Combine(outputPath, Path.GetFileName(AssetDatabase.GetAssetPath(mat))));
                });
            }
        }

        [TabGroup("SPINE", "EXPORT PROJ")]
        [InfoBox("将项目中SkeletonDataAsset所在文件夹与导出文件夹名的同步，方便用WinMerge替换")]
        [Button("SYNC")]
        void SyncProjectFolderNames()
        {
            var uniqueNameMap = new Dictionary<string, int>();

            foreach (var dataAsset in _dataAssets)
            {
                string folderPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(dataAsset)).Replace('\\', '/');
                
                string outputPath = $"{dataAsset.name}";
                uniqueNameMap.TryAdd(outputPath, -1);
                outputPath = $"{dataAsset.name}_{++uniqueNameMap[outputPath]}";

                string folderParentPath = folderPath.Substring(0, folderPath.TrimEnd('/').LastIndexOf('/'));

                AssetDatabase.MoveAsset(folderPath, $"{folderParentPath}/{outputPath}");
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        [TabGroup("SPINE", "NAME CONFLICT")]
        [InfoBox("修复Spine资源重名时，出现命名后缀_d1，使得Asset与Object命名不一致的情况")]
        [SerializeField]
        private List<SkeletonDataAsset> _skeletonDataAssets;

        [TabGroup("SPINE", "NAME CONFLICT")]
        [SerializeField]
        private string _outputPath;

        [TabGroup("SPINE", "NAME CONFLICT")] [SerializeField]
        private bool _putMaterialsTogether;

        [TabGroup("SPINE", "NAME CONFLICT")]
        [Button("RESOLVE")]
        private void Resolve()
        {
            string sharedMaterialsPath = null;
            if (_putMaterialsTogether)
            {
                sharedMaterialsPath = Path.Combine(_outputPath, "materials");
                if (!AssetDatabase.IsValidFolder(sharedMaterialsPath))
                {
                    AssetDatabase.CreateFolder(_outputPath, "materials");
                }
            }
            
            var uniqueNameMap = new Dictionary<string, int>();
            
            foreach (var dataAsset in _skeletonDataAssets)
            {
                string assetName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(dataAsset));
                uniqueNameMap.TryAdd($"{dataAsset.name}", -1);
                string newFolderName = $"{dataAsset.name}_{++uniqueNameMap[$"{dataAsset.name}"]}";
                
                AssetDatabase.CreateFolder(_outputPath, newFolderName);
                string newFolderPath = Path.Combine(_outputPath, newFolderName);

                foreach (var atlasAsset in dataAsset.atlasAssets)
                {
                    // 移动 AtlasAsset 文件
                    string oldPath = AssetDatabase.GetAssetPath(atlasAsset);
                    string atlasAssetFileName = Path.GetFileName(oldPath);
                    AssetDatabase.MoveAsset(oldPath, Path.Combine(newFolderPath, atlasAssetFileName));
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(atlasAsset), atlasAsset.name);

                    // 移动 AtlasAsset , 引用的 AtlasFile
                    oldPath = AssetDatabase.GetAssetPath(((SpineAtlasAsset)atlasAsset).atlasFile);
                    atlasAssetFileName = Path.GetFileName(oldPath);
                    AssetDatabase.MoveAsset(oldPath, Path.Combine(newFolderPath, atlasAssetFileName));

                    atlasAssetFileName = FixNameHelper.Remove_d(atlasAssetFileName);
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(((SpineAtlasAsset)atlasAsset).atlasFile), atlasAssetFileName);


                    // 如果需要 , 移动 AtlasAsset , 引用的 materials
                    foreach (var material in atlasAsset.Materials)
                    {
                        string materialAssetPath = materialAssetPath = AssetDatabase.GetAssetPath(material);
                        string materialFileName = Path.GetFileName(AssetDatabase.GetAssetPath(material));
                        
                        string newMaterialFolderPath = null;
                        
                        if (_putMaterialsTogether)
                        {
                            if (materialAssetPath.Contains(sharedMaterialsPath))
                                continue;

                            string newSubFolderName =
                                Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(material));
                            AssetDatabase.CreateFolder(sharedMaterialsPath, newSubFolderName);
                            
                            newMaterialFolderPath = Path.Combine(sharedMaterialsPath, newSubFolderName);
                        }
                        else
                        {
                            newMaterialFolderPath = newFolderPath;
                        }
                        
                        AssetDatabase.MoveAsset(materialAssetPath,
                            Path.Combine(newMaterialFolderPath, materialFileName));
                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(material), material.name);

                        // 移动 Material 引用的 Texture
                        Texture texture = material.mainTexture;
                        string texturePath = AssetDatabase.GetAssetPath(texture);
                        string textureFileName = Path.GetFileName(AssetDatabase.GetAssetPath(texture));

                        AssetDatabase.MoveAsset(texturePath, Path.Combine(newMaterialFolderPath, textureFileName));
                        
                        textureFileName = FixNameHelper.Remove_d(textureFileName);
                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(texture), textureFileName);
                    }
                }

                // 移动 Skeleton Json 文件
                {
                    string oldPath = AssetDatabase.GetAssetPath(dataAsset.skeletonJSON);
                    string jsonFileName = Path.GetFileName(oldPath);
                    AssetDatabase.MoveAsset(oldPath, Path.Combine(newFolderPath, jsonFileName));
                    
                    jsonFileName = FixNameHelper.Remove_d(jsonFileName);

                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(dataAsset.skeletonJSON), jsonFileName);
                }

                {
                    string oldPath = AssetDatabase.GetAssetPath(dataAsset);
                    string dataAssetFileName = Path.GetFileName(oldPath);
                    AssetDatabase.MoveAsset(oldPath, Path.Combine(newFolderPath, dataAssetFileName));
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(dataAsset), dataAsset.name);
                }
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

#endif