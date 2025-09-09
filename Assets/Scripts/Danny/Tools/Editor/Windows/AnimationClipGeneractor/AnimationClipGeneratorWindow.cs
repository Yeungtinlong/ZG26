using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public class AnimationClipGeneratorWindow : OdinEditorWindow
    {
        private const string GROUP_NAME = "Animation Clip Generator";

        [MenuItem("Danny/Tools/AnimationClip Generator Window")]
        static void OpenWindow()
        {
            var window = GetWindow<AnimationClipGeneratorWindow>();
            window.Show();
            window.SetDefault();
        }
        [InfoBox("产生图集 -> 产生Clip -> 修改锚点 -> 将Clip对应到OverrideController，并且产生最终预制体")]
        [TabGroup(GROUP_NAME, "Sprite Atlas")] [SerializeField]
        private List<Object> _folders;

        [TabGroup(GROUP_NAME, "Sprite Atlas")] [SerializeField]
        private List<string> _texture2DFolderPaths;

        [TabGroup(GROUP_NAME, "Sprite Atlas")] [SerializeField]
        private List<SpriteAtlas> _createdSpriteAtlasList;

        [TabGroup(GROUP_NAME, "Sprite Atlas")] [SerializeField]
        private bool _changeTexture2DSize;

        [TabGroup(GROUP_NAME, "Sprite Atlas")] [ValueDropdown(nameof(TexelListGetter))] [SerializeField]
        private int _texture2DMaxTextureSize = 2048;

        [TabGroup(GROUP_NAME, "Sprite Atlas")] [ValueDropdown(nameof(TexelListGetter))] [SerializeField]
        private int _spriteAtlasMaxTextureSize = 2048;

        [TabGroup(GROUP_NAME, "Sprite Atlas")] [SerializeField]
        private TextureImporterFormat _spriteAtlasFormat = TextureImporterFormat.ASTC_6x6;

        [TabGroup(GROUP_NAME, "Sprite Atlas")] [ValueDropdown(nameof(PlatformValues))] [SerializeField]
        private string _platform;

        private void SetDefault()
        {
            _platform = $"{EditorUserBuildSettings.activeBuildTarget}";
            if (_platform == "iOS")
            {
                _platform = "iPhone";
            }
        }

        private List<int> TexelListGetter()
        {
            return new List<int> { 64, 128, 256, 512, 1024, 2048, 4096, 8192 };
        }

        private List<string> PlatformValues()
        {
            return new List<string> { "Android", "WebGL", "iPhone" };
        }

        [TabGroup(GROUP_NAME, "Sprite Atlas")]
        [Button("SCAN TEXTURE2D PATHS BY FOLDERS")]
        private void ScanTexture2DPathsByFolder()
        {
            _texture2DFolderPaths = AnimationClipGeneratorLogic.ScanTexture2DPathsByFolders(_folders);
        }

        [TabGroup(GROUP_NAME, "Sprite Atlas")]
        [Button("GENERATE")]
        private void GenerateSpriteAtlasList()
        {
            _createdSpriteAtlasList = AnimationClipGeneratorLogic.GenerateSpriteAtlasList(_texture2DFolderPaths,
                new GenerateSpriteAtlasSettings
                {
                    IsChangeTexture2DSize = _changeTexture2DSize,
                    TextureSize = _texture2DMaxTextureSize,
                    SpriteAtlasSize = _spriteAtlasMaxTextureSize,
                    SpriteAtlasFormat = _spriteAtlasFormat,
                    Platform = _platform
                });
        }

        [PropertySpace(20)]
        [TabGroup(GROUP_NAME, "Sprite Atlas")]
        [Button("SCAN CREATED SPRITE ATLAS BY FOLDERS")]
        private void ScanCreatedSpriteAtlasByFolder()
        {
            _createdSpriteAtlasList = AnimationClipGeneratorLogic.ScanSpriteAtlasByFolders(_folders);
        }

        [TabGroup(GROUP_NAME, "Sprite Atlas")]
        [Button("CHANGE SETTINGS")]
        private void ChangeSpriteAtlasSettings()
        {
            AnimationClipGeneratorLogic.ChangeSpriteAtlasSettings(_createdSpriteAtlasList,
                new GenerateSpriteAtlasSettings
                {
                    IsChangeTexture2DSize = _changeTexture2DSize,
                    TextureSize = _texture2DMaxTextureSize,
                    SpriteAtlasSize = _spriteAtlasMaxTextureSize,
                    SpriteAtlasFormat = _spriteAtlasFormat,
                    Platform = _platform
                });
        }

        [TabGroup(GROUP_NAME, "Sprite Atlas")]
        [Button("DELETE GENERATED")]
        private void DeleteGeneratedSpriteAtlas()
        {
            foreach (var spriteAtlas in _createdSpriteAtlasList)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(spriteAtlas));
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            _createdSpriteAtlasList.Clear();
        }

        [TabGroup(GROUP_NAME, "Animation Clip")] [SerializeField]
        private List<SpriteAtlas> _spriteAtlasList;

        [TabGroup(GROUP_NAME, "Animation Clip")] [SerializeField]
        private int _frameRate;

        [TabGroup(GROUP_NAME, "Animation Clip")]
        [Button("GENERATE OR OVERRIDE")]
        private void GenerateClipsBySpriteAtlas()
        {
            AnimationClipGeneratorLogic.GenerateClipsBySpriteAtlas(_spriteAtlasList, _frameRate);
        }

        [TabGroup(GROUP_NAME, "Sprite Pivot")] [SerializeField]
        private List<Object> _spritesContainerFolders;

        /// <summary>
        /// 其余目录下的pivot均参考同级第0个目录下的第0个资源
        /// </summary>
        [TabGroup(GROUP_NAME, "Sprite Pivot")] [SerializeField]
        private bool _isChangeBrotherFolders;

        [TabGroup(GROUP_NAME, "Sprite Pivot")]
        [Button("SET")]
        private void Set()
        {
            AnimationClipGeneratorLogic.SyncSpritePivots(_spritesContainerFolders, _isChangeBrotherFolders);
        }

        [TabGroup(GROUP_NAME, "Animation Override Controller")] [SerializeField]
        private List<Object> _animatorOverrideControllerFolders;

        [TabGroup(GROUP_NAME, "Animation Override Controller")] [SerializeField]
        private RuntimeAnimatorController _virtualController;

        [TabGroup(GROUP_NAME, "Animation Override Controller")] [SerializeField]
        private GameObject _virtualViewPrefab;
        
        [TabGroup(GROUP_NAME, "Animation Override Controller")] [SerializeField]
        private GameObject _virtualLogicPrefab;
        
        [TabGroup(GROUP_NAME, "Animation Override Controller")] [SerializeField]
        private string _logicPrefabVariantTargetPath;

        [TabGroup(GROUP_NAME, "Animation Override Controller")]
        [Button("Generate Animator Override Controllers")]
        private void GenerateAnimatorOverrideControllerFolders()
        {
            AnimationClipGeneratorLogic.GenerateOverrideAnimationControllerAndPrefabVariant(_animatorOverrideControllerFolders, _virtualController, _virtualViewPrefab, _virtualLogicPrefab, _logicPrefabVariantTargetPath);
        }
    }
}