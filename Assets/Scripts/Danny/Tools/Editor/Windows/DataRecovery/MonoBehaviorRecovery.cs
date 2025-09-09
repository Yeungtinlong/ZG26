using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public class MonoBehaviorRecovery : OdinEditorWindow
    {
        [MenuItem("Danny/Tools/MonoBehavior Recovery")]
        static void OpenWindow()
        {
            GetWindow<MonoBehaviorRecovery>().Show();
        }

        [TabGroup(tab: "Single", GroupID = "Json")]
        [InfoBox("The UnityObject, such as ScriptableObject, MonoBehaviour that you want to recover the data to.")]
        [SerializeField]
        private Object _objectToRecover;
        
        private Type _selectedType;

        private IList<string> TypesGetter()
        {
            List<string> result = new List<string>();

            var types = ReflectionHelper.GetTypes(t => typeof(ScriptableObject).IsAssignableFrom(t));
            foreach (var type in types)
            {
                result.Add(string.IsNullOrEmpty(type.Namespace)
                    ? $"{type.Name}"
                    : $"{type.Namespace}.{type.Name}");
            }

            return result;
        }

        private void TypeSetter()
        {
            int lastIndexOfDot = _scriptableObjectType.LastIndexOf('.');
            string selectedNamespace = lastIndexOfDot == -1 ? null : _scriptableObjectType.Substring(0, lastIndexOfDot);
            string selectedTypeName = lastIndexOfDot == -1
                ? _scriptableObjectType
                : _scriptableObjectType.Substring(lastIndexOfDot + 1);

            var types = ReflectionHelper.GetTypes(t => t.Namespace == selectedNamespace && t.Name == selectedTypeName);

            if (types.Count != 1)
            {
                _selectedType = null;
                _scriptableObjectType = null;
                throw new Exception("Set type error.");
            }

            _selectedType = types[0];
        }

        [TabGroup(tab: "Single", GroupID = "Json")] [TextArea(6, 60)] [InfoBox("Paste Json text")] [SerializeField]
        private string _jsonText;

        [TabGroup(tab: "Single", GroupID = "Json")]
        [Button("Deserialize")]
        void DeserializeSingle()
        {
            Undo.RecordObject(_objectToRecover, "Before Recovery");

            try
            {
                JsonConvert.PopulateObject(_jsonText, _objectToRecover);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            
            EditorUtility.SetDirty(_objectToRecover);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [TabGroup(tab: "Multi", GroupID = "Json")]
        [InfoBox("The type, inherit from ScriptableObject, you need to recover.")]
        [SerializeField]
        [ValueDropdown(nameof(TypesGetter)), OnValueChanged(nameof(TypeSetter))]
        private string _scriptableObjectType;
        
        [TabGroup(tab: "Multi", GroupID = "Json")] [InfoBox("Assign Json files")] [SerializeField]
        private List<TextAsset> _jsonTextAssets;

        [TabGroup(tab: "Multi", GroupID = "Json")] [SerializeField]
        private Object _outputFolder;

        [TabGroup(tab: "Multi", GroupID = "Json")] [SerializeField]
        private string _prefixName;
        
        [TabGroup(tab: "Multi", GroupID = "Json")] [SerializeField]
        [ValueDropdown(nameof(FieldsGetter)), OnValueChanged(nameof(FieldSetter))]
        private string _postfixFieldName;
        
        private FieldInfo _selectedField;

        private List<string> FieldsGetter()
        {
            if (_selectedType == null)
            {
                return null;
            }
            
            List<string> result = new List<string>();

            var fields = _selectedType.GetFields();
            
            foreach (var field in fields)
            {
                if (field.FieldType.IsPrimitive || field.FieldType == typeof(string) || field.FieldType.IsEnum)
                {
                    result.Add(field.Name);
                }
            }

            return result;
        }

        private void FieldSetter()
        {
            _selectedField = _selectedType.GetField(_postfixFieldName);
            if (_selectedField == null)
            {
                _postfixFieldName = null;
                throw new Exception("Set field error.");
            }
        }
        
        [TabGroup(tab: "Multi", GroupID = "Json")]
        [Button("Deserialize")]
        void DeserializeAll()
        {
            TypeSetter();
            FieldSetter();
            
            string folderPath = AssetDatabase.GetAssetPath(_outputFolder);

            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                throw new Exception("Output folder is not valid.");
            }
            
            if (_selectedField == null)
            {
                throw new Exception($"Postfix Field not found.");
            }

            foreach (var jsonTextAsset in _jsonTextAssets)
            {
                string json = jsonTextAsset.text;
                ScriptableObject obj = CreateInstance(_selectedType);
                
                JsonConvert.PopulateObject(json, obj);
                
                object postfix = _selectedField.GetValue(obj);
                obj.name = string.IsNullOrEmpty(_prefixName) ? $"{postfix}" : $"{_prefixName}_{postfix}";
                
                AssetDatabase.CreateAsset(obj, $"{folderPath}/{obj.name}.asset");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}