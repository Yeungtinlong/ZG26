// #define DANNY_YAML_SUPPORT
#if DANNY_YAML_SUPPORT
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using YamlDotNet.Serialization;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public class SpriteAtlasFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/SpriteAtlas Fixer")]
        static void OpenWindow()
        {
            GetWindow<SpriteAtlasFixer>().Show();
        }

        [BoxGroup("Get Yaml")] [SerializeField]
        private Object _assetObject;

        // [BoxGroup("Get Yaml")]
        // [OdinSerialize] private Dictionary<object, object> _result;

        [BoxGroup("Get Yaml")] [TextArea(6, 12)] [SerializeField]
        private string _resultText;

        [BoxGroup("Get Yaml")]
        [Button("Deserialize Asset Object")]
        void DeserializeAssetObject()
        {
            throw new NotImplementedException("This function is not implemented.");
        }

        [BoxGroup("Get Yaml")] [ValueDropdown("GetAllVisitors")] [OdinSerialize]
        private string _rootObjectVisitor;

        List<string> GetAllVisitors()
        {
            return TypeCache.GetTypesDerivedFrom<IRootObjectVisitor>()
                .Select(i => i.Name)
                .ToList();
        }


        [BoxGroup("Get Yaml")]
        [Button("Deserialize Asset Object Meta")]
        void DeserializeAssetObjectMeta()
        {
            if (_assetObject == null || string.IsNullOrEmpty(_rootObjectVisitor))
            {
                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(_assetObject);
            string absPath = Path.GetFullPath(assetPath);
            string metaAbsPath = $"{absPath}.meta";
            string yamlContent = File.ReadAllText(metaAbsPath);

            DeserializerBuilder deserializerBuilder = new DeserializerBuilder();
            IDeserializer deserializer = deserializerBuilder.Build();
            var yamlObject = deserializer.Deserialize(new StringReader(yamlContent));

            Debug.Log(yamlObject?.GetType());

            // _result = (Dictionary<object, object>) yamlObject;

            var serializer = new SerializerBuilder()
                .JsonCompatible()
                .Build();

            string json = serializer.Serialize(yamlObject);

            JObject jObject = JObject.Parse(json);

            IRootObjectVisitor rootObjectVisitor = null;

            if (Type.GetType($"{Assembly.GetExecutingAssembly().GetTypes().First(t => t.Name == _rootObjectVisitor)}")
                    is { } type && typeof(IRootObjectVisitor).IsAssignableFrom(type))
            {
                rootObjectVisitor = Activator.CreateInstance(type) as IRootObjectVisitor;
            }
            else
            {
                Debug.LogError($"{_rootObjectVisitor} has not implement from {nameof(IRootObjectVisitor)}.");
                return;
            }

            jObject = rootObjectVisitor.VisitRoot(jObject);
            var dynamicObject = jObject.ToObject();

            serializer = new SerializerBuilder().Build();
            yamlContent = serializer.Serialize(dynamicObject);

            _resultText = yamlContent;
        }

        [BoxGroup("Export Yaml")] [SerializeField]
        private string _outputPath;

        [BoxGroup("Export Yaml")]
        [Button("Export")]
        private void ExportNewYaml()
        {
            if (string.IsNullOrEmpty(_resultText))
            {
                Debug.LogError("Text is null or empty.");
                return;
            }

            try
            {
                File.WriteAllText(_outputPath, _resultText);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
#endif