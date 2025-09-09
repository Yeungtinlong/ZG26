using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class ModelSelector : OdinEditorWindow
{
    [MenuItem("Tools/Model Selector")]
    public static void OpenWindow()
    {
        GetWindow<ModelSelector>().Show();
    }
    
    [SerializeField] private List<Object> _models;
    [SerializeField] private Object _directory;
    
    [Button("SELECT MODELS")]
    private void SelectModels()
    {
        _models.Clear();
        
        string directoryPath = AssetDatabase.GetAssetPath(_directory);
        
        if (!AssetDatabase.IsValidFolder(directoryPath))
        {
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:model", new[] { directoryPath });

        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            if (AssetImporter.GetAtPath(assetPath) is not ModelImporter modelImporter)
            {
                continue;
            }
            
            _models.Add(AssetDatabase.LoadAssetAtPath<Object>(assetPath));
        }
    }

    [Button("FIX NORMALS")]
    private void FixNormals()
    {
        foreach (var model in _models)
        {
            string assetPath = AssetDatabase.GetAssetPath(model);
            
            if (AssetImporter.GetAtPath(assetPath) is not ModelImporter modelImporter)
            {
                continue;
            }

            if (modelImporter.importNormals != ModelImporterNormals.Calculate)
            {
                modelImporter.importNormals = ModelImporterNormals.Calculate;
                modelImporter.SaveAndReimport();
            }
        }
        
        AssetDatabase.Refresh();
    }
}