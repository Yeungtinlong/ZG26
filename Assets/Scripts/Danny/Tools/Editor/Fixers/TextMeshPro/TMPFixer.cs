// #define DANNY_TMP_SUPPORT

#if DANNY_TMP_SUPPORT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public class TMPFixer : OdinEditorWindow
    {
        [Serializable]
        private class MainSubAssetsPair
        {
            public TMP_FontAsset MainAsset;
            public List<Object> SubAssets;
        }

        [MenuItem("Danny/Fixers/TMP Fixer")]
        static void CreateWindow()
        {
            TMPFixer fixer = GetWindow<TMPFixer>();
            fixer.minSize = new Vector2(400f, 300f);
            fixer.titleContent = new GUIContent("TMP Fixer");
            fixer.Show();
        }

        [TabGroup("TMP", "Sub Assets Fixer")] [SerializeField] [OnValueChanged(nameof(SearchSubAssets))]
        private List<TMP_FontAsset> _mainAssets;

        [TabGroup("TMP", "Sub Assets Fixer")] [SerializeField]
        private List<MainSubAssetsPair> _mainSubAssetsPairs;

        private void SearchSubAssets()
        {
            _mainSubAssetsPairs.Clear();
            foreach (var mainAsset in _mainAssets)
            {
                List<Object> subAssets = new List<Object>();
                subAssets.Add(mainAsset.material);
                subAssets.AddRange(mainAsset.atlasTextures);
                _mainSubAssetsPairs.Add(new MainSubAssetsPair { MainAsset = mainAsset, SubAssets = subAssets });
            }
        }

        [TabGroup("TMP", "Sub Assets Fixer")]
        [Button("ADD TO MAIN")]
        private void FixSubAssets()
        {
            foreach (var mainSubAssetsPair in _mainSubAssetsPairs)
            {
                if (_mainSubAssetsPairs == null || _mainSubAssetsPairs.Count == 0)
                {
                    return;
                }

                FieldInfo atlasTexturesField =
                    typeof(TMP_FontAsset).GetField("m_AtlasTextures", BindingFlags.Instance | BindingFlags.NonPublic);

                FieldInfo materialField =
                    typeof(TMP_FontAsset).GetField("material", BindingFlags.Instance | BindingFlags.Public);

                if (mainSubAssetsPair.SubAssets.Exists(asset => asset is Texture2D))
                {
                    atlasTexturesField.SetValue(mainSubAssetsPair.MainAsset, null);
                    Texture2D[] texture2Ds = mainSubAssetsPair.SubAssets.OfType<Texture2D>().ToArray();

                    for (int i = 0; i < texture2Ds.Length; i++)
                    {
                        string path = AssetDatabase.GetAssetPath(texture2Ds[i]);
                        string objectName = texture2Ds[i].name;
                        texture2Ds[i] = Instantiate(texture2Ds[i]);
                        texture2Ds[i].name = objectName;
                        bool deleteSuccess = AssetDatabase.DeleteAsset(path);
                        Debug.Log($"Delete {path} {(deleteSuccess ? "Success" : "Fail")}");
                    }

                    if (texture2Ds == null || texture2Ds.Length == 0)
                    {
                        texture2Ds = new Texture2D[] { new Texture2D(1024, 1024) };
                    }

                    atlasTexturesField.SetValue(mainSubAssetsPair.MainAsset, texture2Ds);

                    foreach (var texture2D in texture2Ds)
                    {
                        AssetDatabase.AddObjectToAsset(texture2D, mainSubAssetsPair.MainAsset);
                    }

                    EditorUtility.SetDirty(mainSubAssetsPair.MainAsset);
                    AssetDatabase.SaveAssets();
                }

                if (mainSubAssetsPair.SubAssets.Exists(asset => asset is Material))
                {
                    materialField.SetValue(mainSubAssetsPair.MainAsset, null);
                    Material[] materials = mainSubAssetsPair.SubAssets.OfType<Material>().ToArray();
                    if (materials.Length > 1)
                    {
                        Debug.LogError("Cannot assign more than 1 materials to a TMP_FontAsset");
                    }
                    else
                    {
                        for (int i = 0; i < materials.Length; i++)
                        {
                            string path = AssetDatabase.GetAssetPath(materials[i]);
                            string objectName = materials[i].name;
                            materials[i] = Instantiate(materials[i]);
                            materials[i].name = objectName;
                            bool deleteSuccess = AssetDatabase.DeleteAsset(path);
                            Debug.Log($"Delete {path} {(deleteSuccess ? "Success" : "Fail")}");
                        }

                        materialField.SetValue(mainSubAssetsPair.MainAsset, materials[0]);

                        foreach (var material in materials)
                        {
                            AssetDatabase.AddObjectToAsset(material, mainSubAssetsPair.MainAsset);
                        }

                        EditorUtility.SetDirty(mainSubAssetsPair.MainAsset);
                        AssetDatabase.SaveAssets();
                    }
                }
            }

            AssetDatabase.Refresh();
        }

        [TabGroup("TMP", "Single Fixer")] [SerializeField]
        private TMP_FontAsset _singleFontAsset;

        [TabGroup("TMP", "Single Fixer")]
        [Button("ADD DEFAULT ATLAS TEXTURE")]
        private void AddDefaultAtlasTextureToFontAsset()
        {
            var atlasTexturesField =
                typeof(TMP_FontAsset).GetField("m_AtlasTextures", BindingFlags.Instance | BindingFlags.NonPublic);
            var atlasTexture = new Texture2D(1024, 1024);
            atlasTexture.name = _singleFontAsset.name;
            Texture2D[] atlasTextures = new Texture2D[] { atlasTexture };
            atlasTexturesField.SetValue(_singleFontAsset, atlasTextures);
            EditorUtility.SetDirty(_singleFontAsset);
            AssetDatabase.AddObjectToAsset(atlasTexture, _singleFontAsset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [TabGroup("TMP", "Single Fixer")]
        [Button("REMOVE ATLAS TEXTURE")]
        private void RemoveDefaultAtlasTextureToFontAsset()
        {
            var atlasTexturesField =
                typeof(TMP_FontAsset).GetField("m_AtlasTextures", BindingFlags.Instance | BindingFlags.NonPublic);
            var atlasTexture = new Texture2D(1024, 1024);
            Texture2D[] atlasTextures = (Texture2D[])atlasTexturesField.GetValue(_singleFontAsset);
            atlasTexturesField.SetValue(_singleFontAsset, null);
            EditorUtility.SetDirty(_singleFontAsset);
            AssetDatabase.RemoveObjectFromAsset(atlasTextures[0]);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        // [TabGroup("TMP", "Sub Assets Fixer")]
        // [Button("Remove from Main")]
        // private void RemoveFromMain()
        // {
        //     if (_subAssets == null || _subAssets.Count == 0 || _mainAssets == null)
        //     {
        //         return;
        //     }
        //
        //     foreach (var subAsset in _subAssets)
        //     {
        //         AssetDatabase.RemoveObjectFromAsset(subAsset);
        //     }
        //
        //     AssetDatabase.SaveAssets();
        // }

        [TabGroup("TMP", "Scan TMP")] [SerializeField]
        private List<GameObject> _sceneGamgeObjects;

        [TabGroup("TMP", "Scan TMP")]
        [Button("Scan Scene GameObjects")]
        private void ScanSceneGameObjects()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            GameObject[] sceneRootGameObjects = activeScene.GetRootGameObjects();

            List<Transform> missRtTMPs = new List<Transform>();
            foreach (var rootGameObject in sceneRootGameObjects)
            {
                missRtTMPs.AddRange(Utils.FindAllTBFS(rootGameObject.transform,
                    t => t.GetComponent<RectTransform>() == null
                         && t.GetComponent<TMP_Text>() != null
                ));
            }

            _sceneGamgeObjects = missRtTMPs.Select(t => t.gameObject).ToList();
        }

        [TabGroup("TMP", "Scan TMP")]
        [Button("DESTROY ALL TextContainer")]
        private void ScanPrefabs()
        {
            List<GameObject> prefabs = AssetDatabase.FindAssets("t:prefab", null)?
                .Select(guid =>
                    AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid))
                ).ToList();

            if (prefabs == null)
            {
                return;
            }

            List<GameObject> requiredToFix = new List<GameObject>();
            foreach (var prefab in prefabs)
            {
                AssetDatabaseUtils.ForEachPrefabRoot(prefab, go =>
                {
                    List<Transform> textContainerKeepers = Utils.FindAllTBFS(go.transform,
                        t => t.GetComponent<TextContainer>() != null
                    );
                    foreach (var keeper in textContainerKeepers)
                    {
                        DestroyImmediate(keeper.GetComponent<TextContainer>());
                        Debug.Log($"Fixed {AssetDatabase.GetAssetPath(prefab)}");
                    }
                });
            }

            _sceneGamgeObjects = requiredToFix;
        }
    }
}

#endif