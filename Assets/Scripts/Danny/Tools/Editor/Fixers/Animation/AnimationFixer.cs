using System;
using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public class AnimationFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/Animation Fixer")]
        static void OpenWindow()
        {
            GetWindow<AnimationFixer>().Show();
        }

        [BoxGroup("Testing")]
        [SerializeField]
        private string _getAssetByName;

        [BoxGroup("Testing")]
        [SerializeField]
        private List<Sprite> _resultObjects;

        [BoxGroup("Testing")]
        [Button("Get Asset By Name Testing")]
        void GetAssetByName()
        {
            Texture2D texture2D = AssetDatabaseUtils.GetAssetByName<Texture2D>("Assets", _getAssetByName);
            _resultObjects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture2D))
                .OfType<Sprite>().ToList();
        }

        [SerializeField]
        private List<AnimationClip> _animationClips;

        [SerializeField]
        private List<AnimationClip> _noSameNameClips;

        [SerializeField]
        private List<AnimationClip> _nextStepFailClips;

        [Button("Fix")]
        void Fix()
        {
            if (_animationClips == null || _animationClips.Count == 0)
            {
                return;
            }

            HashSet<AnimationClip> clipSet = new HashSet<AnimationClip>(_animationClips);
            _noSameNameClips = new List<AnimationClip>();

            foreach (var animationClip in clipSet)
            {
                var bindings = AnimationUtility.GetObjectReferenceCurveBindings(animationClip);

                if ((bindings == null || bindings.Length == 0)
                    || bindings[0].type != typeof(SpriteRenderer)
                    || bindings.Length > 1)
                {
                    Debug.LogError($"{animationClip.name} is not the target type.");
                    continue;
                }

                var keyframes = AnimationUtility.GetObjectReferenceCurve(
                    animationClip,
                    EditorCurveBinding.PPtrCurve(string.Empty, typeof(SpriteRenderer),
                        "m_Sprite"));

                if (keyframes != null && keyframes.Length > 0)
                {
                    Debug.LogError($"{animationClip.name} already has valid frames.");
                    continue;
                }

                Object spriteObj = AssetDatabaseUtils.GetAssetByName<Sprite>("Assets", animationClip.name);
                if (spriteObj == null)
                {
                    Debug.LogError($"{animationClip.name} hasn't the same name Sprite.");
                    _noSameNameClips.Add(animationClip);
                    continue;
                }

                ObjectReferenceKeyframe[] objectReferenceKeyframes = new ObjectReferenceKeyframe[]
                {
                    new()
                    {
                        time = 0f,
                        value = spriteObj,
                    },
                };

                AnimationUtility.SetObjectReferenceCurve(
                    animationClip,
                    EditorCurveBinding.PPtrCurve(string.Empty, typeof(SpriteRenderer), "m_Sprite"),
                    objectReferenceKeyframes
                );

                EditorUtility.SetDirty(animationClip);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [Button("Next Step Fix")]
        void NextStepFix()
        {
            if (_noSameNameClips == null || _noSameNameClips.Count == 0)
            {
                return;
            }

            _nextStepFailClips = new List<AnimationClip>();

            foreach (var animationClip in _noSameNameClips)
            {
                Sprite spriteObj = AssetDatabaseUtils.GetAssetByName<Sprite>("Assets", animationClip.name);
                if (spriteObj != null)
                    continue;
                
                Texture2D texture2DObject = AssetDatabaseUtils.GetAssetByName<Texture2D>("Assets", animationClip.name);
                if (texture2DObject == null)
                {
                    _nextStepFailClips.Add(animationClip);
                    continue;
                }

                List<Sprite> sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(texture2DObject))
                    .OfType<Sprite>().ToList();
                
                if (sprites.Count == 0)
                {
                    _nextStepFailClips.Add(animationClip);
                    continue;
                }

                List<ObjectReferenceKeyframe> keyframes = new List<ObjectReferenceKeyframe>();

                for (int i = 0; i < sprites.Count; i++)
                {
                    var keyframe = new ObjectReferenceKeyframe()
                    {
                        time = i * 0.1f,
                        value = sprites[i]
                    };

                    keyframes.Add(keyframe);
                }

                var settings = AnimationUtility.GetAnimationClipSettings(animationClip);
                settings.startTime = 0f;
                settings.stopTime = (keyframes.Count - 1) * 0.1f;
                AnimationUtility.SetAnimationClipSettings(animationClip, settings);

                AnimationUtility.SetObjectReferenceCurve(
                    animationClip,
                    EditorCurveBinding.PPtrCurve(string.Empty, typeof(SpriteRenderer), "m_Sprite"),
                    keyframes.ToArray()
                );

                EditorUtility.SetDirty(animationClip);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}