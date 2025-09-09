using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class SpriteSliceFixer : OdinEditorWindow
    {
        [BoxGroup("Slice Properties")]
        [SerializeField]
        private Vector2Int _sliceSize;

        [BoxGroup("Slice Properties")]
        [SerializeField]
        private int _maxSliceCount;

        [MenuItem("Danny/Fixers/Sprite Slice Fixer")]
        static void OpenWindow()
        {
            GetWindow<SpriteSliceFixer>().Show();
        }

        // [Button("Slice")]
        // private void Slice()
        // {
        //     Texture2D[] texture2Ds = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);
        //
        //     foreach (var texture2D in texture2Ds)
        //     {
        //         string assetPath = AssetDatabase.GetAssetPath(texture2D);
        //         TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        //         textureImporter.isReadable = true;
        //         textureImporter.textureType = TextureImporterType.Sprite;
        //         textureImporter.spriteImportMode = SpriteImportMode.Multiple;
        //
        //         textureImporter.alphaIsTransparency = true;
        //         textureImporter.wrapMode = TextureWrapMode.Clamp;
        //         textureImporter.filterMode = FilterMode.Point;
        //
        //         TextureImporterSettings textureImporterSettings = new TextureImporterSettings();
        //         textureImporter.ReadTextureSettings(textureImporterSettings);
        //         textureImporterSettings.spriteMeshType = SpriteMeshType.FullRect;
        //         textureImporter.SetTextureSettings(textureImporterSettings);
        //
        //         List<SpriteMetaData> spritesheet = textureImporter.spritesheet.ToList();
        //         
        //         int sliceWidth = _sliceSize.x;
        //         int sliceHeight = _sliceSize.y;
        //
        //         for (int i = 0; i < 8; i++)
        //         {
        //             SpriteMetaData spriteMetaData = spritesheet[i];
        //
        //             spriteMetaData.rect = new Rect((i % 3) * sliceWidth, (2 - (i / 3)) * sliceHeight, sliceWidth,
        //                 sliceHeight);
        //             spriteMetaData.pivot = new Vector2(0.5f, 0.5f);
        //             // spriteMetaData.name = $"{texture2D.name}_{i}";
        //
        //             spritesheet[i] = spriteMetaData;
        //         }
        //         
        //         textureImporter.spritesheet = spritesheet.ToArray();
        //         textureImporter.isReadable = false;
        //         AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        //     }
        // }

        [Button("Slice")]
        private void Slice()
        {
            Texture2D[] texture2Ds = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);

            foreach (var texture2D in texture2Ds)
            {
                string assetPath = AssetDatabase.GetAssetPath(texture2D);
                TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                textureImporter.isReadable = true;
                textureImporter.textureType = TextureImporterType.Sprite;
                textureImporter.spriteImportMode = SpriteImportMode.Multiple;

                textureImporter.alphaIsTransparency = true;
                textureImporter.wrapMode = TextureWrapMode.Clamp;
                textureImporter.filterMode = FilterMode.Point;

                TextureImporterSettings textureImporterSettings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(textureImporterSettings);
                textureImporterSettings.spriteMeshType = SpriteMeshType.FullRect;
                textureImporter.SetTextureSettings(textureImporterSettings);

                List<SpriteMetaData> spritesheet = textureImporter.spritesheet.ToList();
                spritesheet = Sort(spritesheet);
                
                int colCount = _sliceSize.x;
                int rowCount = _sliceSize.y;

                float colSize = (float) texture2D.width / colCount;
                float rowSize = (float) texture2D.height / rowCount;
                
                for (int i = 0; i < Mathf.Min(colCount * rowCount, _maxSliceCount); i++)
                {
                    SpriteMetaData spriteMetaData = spritesheet[i];

                    float x = (i % colCount) * colSize;
                    float y = (rowCount - (i / colCount) - 1) * rowSize;
                    
                    spriteMetaData.rect = new Rect(x, y, colSize,
                        rowSize);
                    spriteMetaData.pivot = new Vector2(0.5f, 0.5f);
                    // spriteMetaData.name = $"{texture2D.name}_{i}";
                    // Debug.Log($"Name: {spriteMetaData.name}, Index: {i}");
                    spritesheet[i] = spriteMetaData;
                }

                textureImporter.spritesheet = spritesheet.ToArray();
                textureImporter.isReadable = false;
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }
        }

        [Button("Set Full Rect")]
        static void SetAllSpriteToFullRect()
        {
            List<Texture2D> texture2Ds = AssetDatabaseUtils.GetAllAssetsOfType<Texture2D>("Assets/Resources");
            foreach (var texture2D in texture2Ds)
            {
                string assetPath = AssetDatabase.GetAssetPath(texture2D);
                TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (textureImporter == null)
                {
                    Debug.LogWarning($"{assetPath} has not textureImporter");
                    continue;
                }
                textureImporter.isReadable = true;

                TextureImporterSettings textureImporterSettings = new TextureImporterSettings();
                textureImporter.ReadTextureSettings(textureImporterSettings);
                textureImporterSettings.spriteMeshType = SpriteMeshType.FullRect;
                textureImporter.SetTextureSettings(textureImporterSettings);
                
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            }
        }
        
        static List<SpriteMetaData> Sort(List<SpriteMetaData> spritesheet)
        {
            return spritesheet.OrderBy(spriteMetaData =>
            {
                string[] splits = spriteMetaData.name.Split('_');
                return int.Parse(splits[splits.Length - 1]);
            }).ToList();
        }
    }
}