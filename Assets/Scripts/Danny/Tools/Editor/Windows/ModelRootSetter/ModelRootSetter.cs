using UnityEngine;
using UnityEditor;
using System.Collections;

namespace SupportUtils
{
    public class SetRootMotionNode
    {
        // [MenuItem("Mecanim/SetRootMotionNode")]
        static void DoSetRootMotionNode()
        {
            Object obj = Selection.activeObject;
            if (obj == null)
                return;

            string assetPath = AssetDatabase.GetAssetPath(obj);
            ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
            if (modelImporter == null)
                return;

            SerializedObject modelImporterObj = new SerializedObject(modelImporter);
            SerializedProperty rootNodeProperty =
                modelImporterObj.FindProperty("m_HumanDescription.m_RootMotionBoneName");

            rootNodeProperty.stringValue = "Rot";

            modelImporterObj.ApplyModifiedProperties();
            AssetDatabase.ImportAsset(assetPath);
        }
    }
}