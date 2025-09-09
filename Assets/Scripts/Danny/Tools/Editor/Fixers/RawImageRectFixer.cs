using System;
using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Scene = UnityEngine.SceneManagement.Scene;

namespace SupportUtils
{
    public class RawImageRectFixer : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/RawImage Rect Fixer")]
        static void OpenWindow()
        {
            GetWindow<RawImageRectFixer>().Show();
        }

        [Serializable]
        [Flags]
        public enum ScanAssetType
        {
            None,
            Prefab,
            Scene
        }

        [BoxGroup("Scan Targets")] [SerializeField]
        private ScanAssetType _scanAssetType;

        [BoxGroup("Scan Targets")] [LabelText("Objects have RawImage with Rect(w:0,h:0)")] [SerializeField]
        private List<Object> _result;

        [BoxGroup("Scan Targets")]
        [Button("Scan")]
        void Scan()
        {
            _result = new List<Object>();

            {
                if (_scanAssetType.HasFlag(ScanAssetType.Prefab))
                {
                    List<GameObject> gameObjects = AssetDatabaseUtils.GetAllAssetsOfType<GameObject>();
                    foreach (var gameObject in gameObjects)
                    {
                        AssetDatabaseUtils.ForEachPrefabRoot(gameObject, go =>
                        {
                            List<Transform> targetTransforms = Utils.FindAllTBFS(
                                go.transform,
                                t => t.TryGetComponent(out RawImage rawImage) &&
                                     (rawImage.uvRect.width == 0 || rawImage.uvRect.height == 0)
                            );

                            if (targetTransforms.Count > 0)
                            {
                                _result.Add(gameObject);
                            }
                        }, false);
                    }
                }
            }

            {
                if (_scanAssetType.HasFlag(ScanAssetType.Scene))
                {
                    List<SceneAsset> sceneAssets = AssetDatabaseUtils.GetAllAssetsOfType<SceneAsset>();
                    foreach (var sceneAsset in sceneAssets)
                    {
                        string path = AssetDatabase.GetAssetPath(sceneAsset);
                        Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                        List<GameObject> gameObjects = scene.GetRootGameObjects().ToList();
                        foreach (var gameObject in gameObjects)
                        {
                            List<Transform> targetTransforms = Utils.FindAllTBFS(
                                gameObject.transform,
                                t => t.TryGetComponent(out RawImage rawImage) &&
                                     (rawImage.uvRect.width == 0 || rawImage.uvRect.height == 0)
                            );

                            if (targetTransforms.Count > 0)
                            {
                                _result.Add(sceneAsset);
                                break;
                            }
                        }

                        EditorSceneManager.CloseScene(scene, true);
                    }
                }
            }
        }

        [BoxGroup("Fix Targets")]
        [Button("Fix")]
        void Fix()
        {
            if (_result == null || _result.Count == 0)
                return;

            {
                if (_scanAssetType.HasFlag(ScanAssetType.Prefab))
                {
                    List<GameObject> gameObjects = _result.Where(o => o is GameObject).Cast<GameObject>().ToList();
                    foreach (var gameObject in gameObjects)
                    {
                        AssetDatabaseUtils.ForEachPrefabRoot(gameObject, go =>
                        {
                            List<Transform> targetTransforms = Utils.FindAllTBFS(
                                go.transform,
                                t => t.TryGetComponent(out RawImage rawImage) &&
                                     (rawImage.uvRect.width == 0 || rawImage.uvRect.height == 0)
                            );

                            foreach (var targetTransform in targetTransforms)
                            {
                                RawImage rawImage = targetTransform.GetComponent<RawImage>();
                                Rect rect = rawImage.uvRect;
                                rect.width = rect.height = 1;
                                rawImage.uvRect = rect;
                            }
                        });
                    }
                }
            }

            {
                if (_scanAssetType.HasFlag(ScanAssetType.Scene))
                {
                    List<SceneAsset> sceneAssets = _result.Where(o => o is SceneAsset).Cast<SceneAsset>().ToList();
                    foreach (var sceneAsset in sceneAssets)
                    {
                        string path = AssetDatabase.GetAssetPath(sceneAsset);
                        Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                        List<GameObject> gameObjects = scene.GetRootGameObjects().ToList();
                        foreach (var gameObject in gameObjects)
                        {
                            List<Transform> targetTransforms = Utils.FindAllTBFS(
                                gameObject.transform,
                                t => t.TryGetComponent(out RawImage rawImage) &&
                                     (rawImage.uvRect.width == 0 || rawImage.uvRect.height == 0)
                            );

                            foreach (var targetTransform in targetTransforms)
                            {
                                RawImage rawImage = targetTransform.GetComponent<RawImage>();
                                Rect rect = rawImage.uvRect;
                                rect.width = rect.height = 1;
                                rawImage.uvRect = rect;
                            }

                            EditorUtility.SetDirty(gameObject);
                        }

                        EditorSceneManager.SaveScene(scene);
                        EditorSceneManager.CloseScene(scene, true);
                    }

                    AssetDatabase.SaveAssets();
                }
            }

            AssetDatabase.Refresh();
        }
    }
}