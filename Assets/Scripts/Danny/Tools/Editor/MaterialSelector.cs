using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class MaterialSelector : OdinEditorWindow
    {
        [MenuItem("Danny/Tools/Material Selector")]
        static void OpenWindow()
        {
            GetWindow<MaterialSelector>().Show();
        }

        [SerializeField] private Shader _targetShader;

        [Button("Select all")]
        void SelectionAll()
        {
            List<Material> materials = AssetDatabaseUtils.GetAllAssetsOfType<Material>();
            if (materials == null || materials.Count == 0)
            {
                return;
            }

            Selection.activeObject = materials[0];
            Selection.objects = materials
                .Where(m => m.shader == _targetShader)
                .Cast<Object>()
                .ToArray();

        }
    }
}