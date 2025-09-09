using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class MainObjectNameFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/Main Object Name Fixer")]
        static void OpenWindow()
        {
            GetWindow<MainObjectNameFixer>().Show();
        }

        [BoxGroup("Check Names")] [SerializeField]
        private string _mainObjectName;

        [BoxGroup("Check Names")] [SerializeField]
        private string _assetFileName;

        [BoxGroup("Check Names")] [SerializeField]
        private Object _object;

        [BoxGroup("Check Names")]
        [Button("Check")]
        private void PrintObjectNames()
        {
            _mainObjectName = _assetFileName = "";

            string assetPath = AssetDatabase.GetAssetPath(_object);
            _mainObjectName = _object.name;
            _assetFileName = Path.GetFileNameWithoutExtension(assetPath);
        }

        [BoxGroup("Fix Names")] [SerializeField]
        private List<Object> _objects;

        [BoxGroup("Fix Names")]
        [Button("Fix")]
        private void FixObjectNames()
        {
            foreach (var o in _objects)
            {
                string assetPath = AssetDatabase.GetAssetPath(o);
                string assetFileName = Path.GetFileNameWithoutExtension(assetPath);
                string mainObjectName = o.name;

                if (mainObjectName != assetFileName)
                {
                    o.name = assetFileName;
                    EditorUtility.SetDirty(o);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}