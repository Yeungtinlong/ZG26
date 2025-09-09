using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class NGUIShaderNameFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/NGUI Shader Name Fixer")]
        static void OpenWindow()
        {
            GetWindow<NGUIShaderNameFixer>().Show();
        }

        [SerializeField] private List<Shader> _shaders;
        [SerializeField] private bool _removeHidden;

        [Button("Fix")]
        void Fix()
        {
            foreach (var shader in _shaders)
            {
                string path = AssetDatabase.GetAssetPath(shader);
                string newName = Path.GetFileName(path).Replace("_", " - ");
                newName = _removeHidden ? newName.TrimStart("Hidden - ".ToCharArray()) : newName;
                AssetDatabase.RenameAsset(path, newName);
                EditorUtility.SetDirty(shader);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}