// #define DANNY_LOCALIZATION_SUPPORT
#if DANNY_LOCALIZATION_SUPPORT

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Tables;

namespace SupportUtils
{
    public class LocalizationFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/Localization Fixer")]
        static void OpenFixerWindow()
        {
            CreateWindow<LocalizationFixer>();
        }

        [BoxGroup("SharedTableData - StringTableCollection - StringTable")] [SerializeField]
        private List<TextAsset> _sharedDataJsons;

        [BoxGroup("SharedTableData - StringTableCollection - StringTable")] [SerializeField]
        private List<TextAsset> _stringTableJsons;

        // [BoxGroup("SharedTableData - StringTableCollection - StringTable")] [SerializeField]
        private List<SharedTableData> _sharedTableDatas;

        // [BoxGroup("SharedTableData - StringTableCollection - StringTable")] [SerializeField]
        private List<StringTableCollection> _stringTableCollections;

        [BoxGroup("SharedTableData - StringTableCollection - StringTable")]
        [Button("Fix")]
        private void Fix()
        {
            _sharedTableDatas = AssetDatabaseUtils.GetAllAssetsOfType<SharedTableData>();
            _stringTableCollections = AssetDatabaseUtils.GetAllAssetsOfType<StringTableCollection>();

            if (FixSharedData())
            {
                FixStringTable();
                FixSmartString();
            }
        }

        private void FixSmartString()
        {
            foreach (var stringTableCollection in _stringTableCollections)
            {
                foreach (var stringTable in stringTableCollection.StringTables)
                {
                    foreach (var kvp in stringTable)
                    {
                        if (kvp.Value.Value.Contains("{global"))
                        {
                            kvp.Value.IsSmart = true;
                        }
                    }

                    EditorUtility.SetDirty(stringTable);
                }
            }

            AssetDatabase.SaveAssets();
        }

        private void FixStringTable()
        {
            _sharedTableDatas = AssetDatabaseUtils.GetAllAssetsOfType<SharedTableData>();
            
            if (_stringTableCollections == null)
            {
                return;
            }

            foreach (var stringTableCollection in _stringTableCollections)
            {
                foreach (StringTable stringTable in stringTableCollection.StringTables)
                {
                    var jsonTextAsset = _stringTableJsons.FirstOrDefault(asset => asset.name == stringTable.name);
                    if (jsonTextAsset == null)
                    {
                        throw new Exception($"Can't find TextAsset with name {stringTable.name}.");
                    }

                    // Clear all key value pairs.
                    stringTable.Clear();

                    JObject jObject = JObject.Parse(jsonTextAsset.text);

                    /*
                        **** from Json file ****

                        "m_TableData": [
                            {
                              "m_Id": 119642890240,
                              "m_Localized": "开始新游戏",
                              "m_Metadata": {
                                "m_Items": []
                              }
                            },
                        ]
                     */
                    var tableData = jObject["m_TableData"];
                    foreach (var termItem in tableData)
                    {
                        stringTable.AddEntry((long)termItem["m_Id"], (string)termItem["m_Localized"]);
                    }

                    stringTable.SharedData = _sharedTableDatas.FirstOrDefault(sharedTableData => sharedTableData.name == $"{stringTable.name.Split('_')[0]} Shared Data");
                    
                    EditorUtility.SetDirty(stringTable);
                }
            }

            AssetDatabase.SaveAssets();
        }

        private bool FixSharedData()
        {
            _sharedTableDatas = AssetDatabaseUtils.GetAllAssetsOfType<SharedTableData>();
            
            if (_sharedTableDatas == null)
                return false;

            foreach (SharedTableData sharedTableData in _sharedTableDatas)
            {
                TextAsset sharedDataJson = _sharedDataJsons.FirstOrDefault(jsonFile => jsonFile.name == sharedTableData.name);
                if (sharedDataJson == null)
                {
                    Debug.LogError($"Can't find Json file to match {sharedTableData.name}");
                    return false;
                }
                
                JObject jObject = JObject.Parse(sharedDataJson.text);

                Undo.RecordObject(sharedTableData, "Fix ShareData");

                sharedTableData.Clear();

                foreach (var entry in jObject["m_Entries"])
                {
                    sharedTableData.AddKey((string)entry["m_Key"], (long)entry["m_Id"]);
                }

                string nameGuid = (string)jObject["m_TableCollectionNameGuidString"];

                if (!AssetDatabaseUtils.TryChangeAssetGuid(AssetDatabase.GetAssetPath(sharedTableData), nameGuid, true))
                {
                    Undo.PerformUndo();
                    return false;
                }

                StringTableCollection stringTableCollection = _stringTableCollections.FirstOrDefault(
                    stc =>
                    {
                        string assetPath = AssetDatabase.GetAssetPath(stc);
                        string fileName = Path.GetFileNameWithoutExtension(assetPath);
                        return fileName == sharedTableData.name.Replace(" Shared Data", string.Empty);
                    });

                if (stringTableCollection == null)
                {
                    Debug.LogError($"Can't found {sharedTableData.name} 's StringTableCollection.");
                }

                PropertyInfo sharedData = typeof(StringTableCollection).GetProperty("SharedData",
                    BindingFlags.Instance | BindingFlags.Public);
                sharedData.SetValue(stringTableCollection, sharedTableData);
                
                EditorUtility.SetDirty(stringTableCollection);
                EditorUtility.SetDirty(sharedTableData);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            return true;
        }
    }
}
#endif