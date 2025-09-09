using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public class AssetReferenceUtilities : OdinEditorWindow
    {
        [MenuItem("Danny/Tools/Asset Reference Utilities")]
        static void CreateWindow()
        {
            GetWindow<AssetReferenceUtilities>().Show();
        }

        [InfoBox("通过GUID定位目标Object")]
        [BoxGroup("Target Asset")] [SerializeField]
        private string _guidForSelect;
        
        [BoxGroup("Target Asset")]
        [Button("TARGET")]
        private void SelectByGuid()
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(_guidForSelect);
            Object assetObj = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (assetObj == null)
            {
                if (string.IsNullOrEmpty(assetPath))
                {
                    Debug.LogError($"Can't find object by guid {_guidForSelect}");
                    return;
                }

                Debug.LogError($"{assetPath} is exists, but Object is not exists.");
                return;
            }

            Selection.activeObject = assetObj;
        }
        
        [InfoBox("搜索Object被哪些资源引用")]
        [BoxGroup("Guid from Asset")] [LabelText("File Id:")] [SerializeField]
        private string _originFileId;

        [BoxGroup("Guid from Asset")] [LabelText("Guid:")] [SerializeField]
        private string _originGuid;

        [BoxGroup("Guid from Asset")] [LabelText("Type:")] [SerializeField]
        private string _originTypeId;

        [BoxGroup("Guid from Asset")]
        [LabelText("Guid from:")]
        [OnValueChanged(nameof(OnGuidProviderValueChanged))]
        [SerializeField]
        private Object _guidProvider;

        private void OnGuidProviderValueChanged()
        {
            if (_guidProvider == null)
                return;

            _originGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_guidProvider));
        }

        private string _assetPathToSearch;

        private bool _isSearching = false;

        [BoxGroup("Guid from Asset")] [LabelText("Extension Name")] [SerializeField] [ExtensionName]
        private string _targetFileTypeName;

        [BoxGroup("Guid from Asset")]
        [Button("SEARCH")]
        private void Search()
        {
            if (_isSearching)
                return;
            _isSearching = true;

            ReferenceInfo origin = new ReferenceInfo(_originGuid, _originFileId, _originTypeId);

            _result = new AssetReferenceHelper().SearchFileIdAndGuidReference(
                _assetPathToSearch, ref origin,
                _targetFileTypeName, _guidProvider);

            _isSearching = false;
        }

        [BoxGroup("Guid from Asset")] [OdinSerialize]
        public List<ReferenceResult> _result = new List<ReferenceResult>();

        [BoxGroup("Reference Replacement")] [LabelText("File Id:")] [SerializeField]
        private string _targetFileId;

        [BoxGroup("Reference Replacement")] [LabelText("Guid:")] [SerializeField]
        private string _targetGuid;

        [BoxGroup("Reference Replacement")] [LabelText("Type:")] [SerializeField]
        private string _targetTypeId;

        [BoxGroup("Reference Replacement")]
        [LabelText("Guid from:")]
        [OnValueChanged(nameof(OnTargetGuidProviderValueChanged))]
        [SerializeField]
        private Object _targetGuidProvider;

        private void OnTargetGuidProviderValueChanged()
        {
            if (_targetGuidProvider == null)
                return;

            _targetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_targetGuidProvider));
        }
        
        [BoxGroup("Reference Replacement")]
        [Button("REPLACE")]
        private void Alternative()
        {
            if (_isSearching || _result == null || _result.Count == 0)
                return;

            var origin = new ReferenceInfo(_originGuid, _originFileId, _originTypeId);
            var target = new ReferenceInfo(_targetGuid, _targetFileId, _targetTypeId);

            new AssetReferenceHelper().AlternateScriptLocation(_assetPathToSearch,
                ref origin, ref target,
                _result);
        }

        [InfoBox("搜索多个Object被哪些资源引用")]
        [BoxGroup("Batch Search")] 
        [SerializeField] private List<Object> _guidProviders = new List<Object>();

        [BoxGroup("Batch Search")] [LabelText("Extension Name")] [SerializeField] [ExtensionName]
        private string _batchExtensionName;
        
        [BoxGroup("Batch Search")] [OdinSerialize]
        // [Sirenix.OdinInspector.ReadOnly]
        public List<ReferenceResult> _batchResults = new List<ReferenceResult>();
        
        [BoxGroup("Batch Search")]
        [Button("SEARCH")]
        private void SearchBatch()
        {
            _batchResults = new AssetReferenceHelper().SearchReferences(_assetPathToSearch, _batchExtensionName,
                _guidProviders);
        }
    }
}