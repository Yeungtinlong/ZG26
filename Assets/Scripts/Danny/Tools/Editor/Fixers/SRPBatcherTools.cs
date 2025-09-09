using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SupportUtils
{
    public class SRPBatcherTools : OdinEditorWindow
    {
        [MenuItem("Danny/Tools/SRP Batcher Tools")]
        static void OpenWindow()
        {
            GetWindow<SRPBatcherTools>().Show();
        }

        [SerializeField] private List<Material> _withoutSpecularMapMaterials;
        [SerializeField] private List<Material> _withSpecularMapMaterials;

        [Button("SCAN SIMPLE LIT WITH SPECULAR IN SCENE")]
        private void ScanAllSimpleLitSpecularMaterialsInScene()
        {
            HashSet<Material> withoutSpecularMapMaterials = new HashSet<Material>();
            HashSet<Material> withSpecularMapMaterials = new HashSet<Material>();

            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (var rootGameObject in rootGameObjects)
            {
                Renderer[] renderers = rootGameObject.GetComponentsInChildren<Renderer>();

                foreach (var renderer in renderers)
                {
                    Material[] materials = renderer.sharedMaterials;

                    foreach (var material in materials)
                    {
                        Shader shader = material.shader;
                        if (shader != Shader.Find("Universal Render Pipeline/Simple Lit"))
                        {
                            continue;
                        }

                        var highlightMap = material.GetTexture("_SpecGlossMap");
                        // _SpecularHighlights == 0，即开启高光反射
                        if (highlightMap == null && Mathf.Approximately(material.GetFloat("_SpecularHighlights"), 0f))
                        {
                            withoutSpecularMapMaterials.Add(material);
                            continue;
                        }

                        if (highlightMap != null)
                        {
                            withSpecularMapMaterials.Add(material);
                            continue;
                        }
                    }
                }
            }

            _withoutSpecularMapMaterials = withoutSpecularMapMaterials.ToList();
            _withSpecularMapMaterials = withSpecularMapMaterials.ToList();
        }
        
        [Button("SCAN SIMPLE LIT WITH SPECULAR IN PROJECT")]
        private void ScanAllSimpleLitSpecularMaterialsInProject()
        {
            HashSet<Material> withoutSpecularMapMaterials = new HashSet<Material>();
            HashSet<Material> withSpecularMapMaterials = new HashSet<Material>();

            List<Material> projectMaterials = AssetDatabaseUtils.GetAllAssetsOfType<Material>();
            
            foreach (var material in projectMaterials)
            {
                Shader shader = material.shader;
                if (shader != Shader.Find("Universal Render Pipeline/Simple Lit"))
                {
                    continue;
                }

                var highlightMap = material.GetTexture("_SpecGlossMap");
                // _SpecularHighlights == 0，即开启高光反射
                if (highlightMap == null && Mathf.Approximately(material.GetFloat("_SpecularHighlights"), 0f))
                {
                    withoutSpecularMapMaterials.Add(material);
                    continue;
                }

                if (highlightMap != null)
                {
                    withSpecularMapMaterials.Add(material);
                    continue;
                }
            }

            _withoutSpecularMapMaterials = withoutSpecularMapMaterials.ToList();
            _withSpecularMapMaterials = withSpecularMapMaterials.ToList();
        }

        [Button("TURN OFF ALL SPECULAR")]
        private void TurnOffAllSpecular()
        {
            foreach (var material in _withoutSpecularMapMaterials)
            {
                Shader shader = material.shader;
                if (shader != Shader.Find("Universal Render Pipeline/Simple Lit"))
                {
                    continue;
                }

                material.SetFloat("_SpecularHighlights", 1f);

                EditorUtility.SetDirty(material);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}