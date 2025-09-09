using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class TextTMPConverterWindow : OdinEditorWindow
    {
        [MenuItem("Danny/Tools/Text TMP Converter")]
        static void OpenWindow()
        {
            GetWindow<TextTMPConverterWindow>().Show();
        }

        [SerializeField] private TMP_FontAsset _tmpFontAsset;

        [Button("CONVERT ALL")]
        private void ConvertAllTextToTMP()
        {
            TextTMPConverterLogic.ConvertAllProjectTextToTMP(_tmpFontAsset);
        }
        
        [SerializeField] private GameObject _prefab;
        
        [Button("CONVERT PREFAB")]
        private void ConvertPrefabTextToTMP()
        {
            TextTMPConverterLogic.ConvertPrefabTextToTMP(_prefab, _tmpFontAsset);
        }
    }
}