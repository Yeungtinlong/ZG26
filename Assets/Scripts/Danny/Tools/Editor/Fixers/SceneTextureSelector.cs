using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneTextureSelector : OdinEditorWindow
{
    [MenuItem("Tools/Scene Texture Selector")]
    static void OpenWindow()
    {
        GetWindow<SceneTextureSelector>().Show();
    }

    [SerializeField] private List<Texture2D> _texture2Ds;

    [SerializeField] private Shader _targetShader;

    [Button("SELECT TEXTURES")]
    private void SelectTextures()
    {
        // SceneManager.GetActiveScene().GetRootGameObjects();

        HashSet<Texture2D> texMap = new HashSet<Texture2D>();

        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();

        foreach (var renderer in renderers)
        {
            var sharedMaterials = renderer.sharedMaterials;

            foreach (var sharedMaterial in sharedMaterials)
            {
                if (sharedMaterial.shader != _targetShader)
                {
                    continue;
                }

                var baseMap = (Texture2D)sharedMaterial.GetTexture("_BaseMap");
                var bumpMap = (Texture2D)sharedMaterial.GetTexture("_BumpMap");

                if (baseMap != null)
                {
                    texMap.Add(baseMap);
                }

                if (bumpMap != null)
                {
                    texMap.Add(bumpMap);
                }
            }
        }

        _texture2Ds = texMap.ToList();
    }

    [SerializeField] private Object _targetDirectory;

    [Button("MOVE")]
    private void MoveToNewDirectory()
    {
        foreach (var texture2D in _texture2Ds)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture2D);

            AssetDatabase.MoveAsset(assetPath,
                $"{AssetDatabase.GetAssetPath(_targetDirectory)}/{Path.GetFileName(assetPath)}");
        }

        AssetDatabase.Refresh();
    }

    [SerializeField] private TextureImporterFormat _targetFormatForAlphaTextures;
    [SerializeField] private TextureImporterFormat _targetFormatForRGBTextures;

    [Button("CHANGE COMPRESS FORMAT")]
    private void ChangeCompressionFormat()
    {
        foreach (var texture2D in _texture2Ds)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture2D);

            if (AssetImporter.GetAtPath(assetPath) is not TextureImporter textureImporter)
            {
                continue;
            }

            bool isRGBA = textureImporter.DoesSourceTextureHaveAlpha();
            
            TextureImporterPlatformSettings textureImporterPlatformSettings = textureImporter.GetPlatformTextureSettings("Android");
            textureImporterPlatformSettings.format = isRGBA ? _targetFormatForAlphaTextures : _targetFormatForRGBTextures;
            textureImporterPlatformSettings.compressionQuality = 0;
            textureImporter.SetPlatformTextureSettings(textureImporterPlatformSettings);
            textureImporter.SaveAndReimport();
        }

        AssetDatabase.Refresh();
    }

    [Button("CHECK ALPHA CHANNEL")]
    private void CheckAlphaChannel()
    {
        foreach (var texture2D in _texture2Ds)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture2D);

            if (AssetImporter.GetAtPath(assetPath) is not TextureImporter textureImporter)
            {
                continue;
            }
            
            Debug.Log($"{texture2D.name} has alpha channel? {textureImporter.DoesSourceTextureHaveAlpha()}");
        }
    }
}