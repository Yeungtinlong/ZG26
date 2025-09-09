// #define DANNY_INPUTSYSTEM_SUPPORT
#if DANNY_INPUTSYSTEM_SUPPORT
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemFixer : EditorWindow
{
    private InputActionAsset _assetToConvert;

    [MenuItem("Danny/Fixers/InputSystem Fixer")]
    static void OpenFixerWindow()
    {
        CreateWindow<InputSystemFixer>();
    }

    private void OnGUI()
    {
        _assetToConvert =
            (InputActionAsset) EditorGUILayout.ObjectField(_assetToConvert, typeof(InputActionAsset), false);

        if (GUILayout.Button("Fix"))
        {
            ConvertAssetToJson();
        }
    }

    private void ConvertAssetToJson()
    {
        string path = AssetDatabase.GetAssetPath(_assetToConvert);
        string jsonContent = _assetToConvert.ToJson();

        string assetPath = path.TrimStart("Assets/".ToCharArray());
        assetPath = assetPath.TrimEnd(".asset".ToCharArray());
        
        try
        {
            File.WriteAllText(System.IO.Path.Combine(Application.dataPath, $"{assetPath}.{InputActionAsset.Extension}"), jsonContent);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif