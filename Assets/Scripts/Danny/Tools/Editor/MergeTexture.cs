using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class MergeTexture : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/Merge Texture")]
        static void OpenWindow()
        {
            GetWindow<MergeTexture>().Show();
        }

        [SerializeField] private Texture2D _rgbTex;
        [SerializeField] private Texture2D _alphaTex;
        [SerializeField] private bool _replaceOrigin;
        [SerializeField] private bool _forceSetReadable;

        [Button("Merge")]
        void Merge()
        {
            if (_rgbTex == null || _alphaTex == null)
            {
                Debug.LogError($"Texture is empty.");
                return;
            }

            string rgbPath = AssetDatabase.GetAssetPath(_rgbTex);
            var rgbImporter = AssetImporter.GetAtPath(rgbPath);
            if (rgbImporter is TextureImporter rgbTexImporter)
            {
                rgbTexImporter.isReadable = _forceSetReadable;
                rgbTexImporter.SaveAndReimport();
            }

            string alphaPath = AssetDatabase.GetAssetPath(_alphaTex);
            var alphaImporter = AssetImporter.GetAtPath(alphaPath);
            if (alphaImporter is TextureImporter alphaTexImporter)
            {
                alphaTexImporter.isReadable = _forceSetReadable;
                alphaTexImporter.SaveAndReimport();
            }

            if (!_rgbTex.isReadable || !_alphaTex.isReadable)
            {
                Debug.LogError("Textures are not all readable.");
                return;
            }

            if (_rgbTex.GetPixels().Length != _alphaTex.GetPixels().Length)
            {
                Debug.LogError("Textures size are not equaled.");
                return;
            }

            var tex = new Texture2D(_rgbTex.width, _rgbTex.height, TextureFormat.RGBA32, false);
            var colors = new Color[_rgbTex.width * _rgbTex.height];
            var rgbColors = _rgbTex.GetPixels();
            var alphaColors = _alphaTex.GetPixels();

            for (int i = 0; i < colors.Length; i++)
            {
                var rgba = rgbColors[i];
                rgba.a = alphaColors[i].r;
                colors[i] = rgba;
            }

            tex.SetPixels(colors);

            string path = AssetDatabase.GetAssetPath(_rgbTex);
            if (!_replaceOrigin)
            {
                path = $"{Path.GetDirectoryName(path)}/{Path.GetFileNameWithoutExtension(path)}-RGBA32.png";
            }

            File.WriteAllBytes(path, tex.EncodeToPNG());

            if (_forceSetReadable)
            {
                rgbTexImporter = (TextureImporter)AssetImporter.GetAtPath(rgbPath);
                rgbTexImporter.isReadable = false;
                rgbTexImporter.SaveAndReimport();

                alphaTexImporter = (TextureImporter)AssetImporter.GetAtPath(alphaPath);
                alphaTexImporter.isReadable = false;
                alphaTexImporter.SaveAndReimport();
            }

            AssetDatabase.Refresh();
        }
    }
}