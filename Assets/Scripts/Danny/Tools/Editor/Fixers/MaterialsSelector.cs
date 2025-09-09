using System.Collections.Generic;
using SupportUtils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class MaterialsSelector : OdinEditorWindow
    {
        [MenuItem("Tools/Materials Selector")]
        static void OpenWindow()
        {
            GetWindow<MaterialsSelector>().Show();
        }


        [SerializeField] private List<Material> _targetMaterials;
        [SerializeField] private List<Material> _nonTargetMaterials;
        [SerializeField] private Shader _shader;
        [SerializeField] private Shader _shaderToReplace;

        [Button("SCAN")]
        private void SelectMaterialsWithShader()
        {
            _targetMaterials.Clear();
            _nonTargetMaterials.Clear();

            List<Material> materials = AssetDatabaseUtils.GetAllAssetsOfType<Material>();

            foreach (var material in materials)
            {
                if (material.shader == _shader)
                {
                    _targetMaterials.Add(material);
                }
                else
                {
                    _nonTargetMaterials.Add(material);
                }
            }
        }

        [Button("REPLACE")]
        private void ReplaceAllMaterialsShader()
        {
            foreach (var material in _targetMaterials)
            {
                material.shader = _shaderToReplace;
                EditorUtility.SetDirty(material);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}