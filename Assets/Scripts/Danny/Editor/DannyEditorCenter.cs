using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace SupportUtils
{
    public class DannyEditorCenter : OdinEditorWindow
    {
        private const string SPINE_MACRO = "DANNY_SPINE_SUPPORT";
        private const string INPUTSYSTEM_MACRO = "DANNY_INPUTSYSTEM_SUPPORT";
        private const string LOCALIZATION_MACRO = "DANNY_LOCALIZATION_SUPPORT";
        private const string TMP_MACRO = "DANNY_TMP_SUPPORT";
        private const string TEST_ASSETBUNDLE = "TEST_ASSETBUNDLE";

        [MenuItem("Danny/Danny Center")]
        static void OpenWindow()
        {
            var window = GetWindow<DannyEditorCenter>("DANNY CENTER");
            window.Show();
            window.CheckMacros();
        }

        private void CheckMacros()
        {
            PlayerSettings.GetScriptingDefineSymbols(
                NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                out string[] defines);
            _spineSupport = defines.Contains(SPINE_MACRO);
            _inputSystemSupport = defines.Contains(INPUTSYSTEM_MACRO);
            _localizationSupport = defines.Contains(LOCALIZATION_MACRO);
            _testAssetbundleSupport = defines.Contains(TEST_ASSETBUNDLE);
        }

        [SerializeField] private bool _spineSupport;
        [SerializeField] private bool _inputSystemSupport;
        [SerializeField] private bool _localizationSupport;
        [SerializeField] private bool _testAssetbundleSupport;

        [Button("SAVE")]
        void Save()
        {
            PlayerSettings.GetScriptingDefineSymbols(
                NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                out string[] defines);

            var definesList = defines.ToList();
            if (_spineSupport && !defines.Contains(SPINE_MACRO))
            {
                definesList.Add(SPINE_MACRO);
            }

            if (_inputSystemSupport && !defines.Contains(INPUTSYSTEM_MACRO))
            {
                definesList.Add(INPUTSYSTEM_MACRO);
            }

            if (_localizationSupport && !defines.Contains(LOCALIZATION_MACRO))
            {
                definesList.Add(LOCALIZATION_MACRO);
            }

            if (_testAssetbundleSupport && !definesList.Contains(TEST_ASSETBUNDLE))
            {
                definesList.Add(TEST_ASSETBUNDLE);
            }

            PlayerSettings.SetScriptingDefineSymbols(
                NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                definesList.ToArray());
        }
    }
}