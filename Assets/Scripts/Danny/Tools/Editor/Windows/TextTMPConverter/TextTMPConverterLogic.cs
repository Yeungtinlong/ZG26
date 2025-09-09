using System;
using System.Collections.Generic;
using System.Linq;
using SupportUtils;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace SupportUtils
{
    public class TextTMPConverterLogic
    {
        private static void ScanGameObjectAndConvertTextToTMP(GameObject gameObject, TMP_FontAsset fontAsset)
        {
            Text[] texts = gameObject.GetComponentsInChildren<Text>(true);
            if (texts == null || texts.Length == 0)
                return;

            List<GameObject> gameObjectWithTextList = texts.Select(text => text.gameObject).ToList();
            foreach (var gameObjectWithText in gameObjectWithTextList)
            {
                Text textComponent = gameObjectWithText.GetComponent<Text>();
                // Text Settings
                string textValue = textComponent.text;
                float fontSize = textComponent.fontSize;
                TextAnchor textAlignment = textComponent.alignment;

                Object.DestroyImmediate(textComponent);

                // TextMeshPro Settings
                TextMeshProUGUI textMeshProUGUI = gameObjectWithText.AddComponent<TextMeshProUGUI>();
                textMeshProUGUI.font = fontAsset;

                textMeshProUGUI.text = textValue;
                textMeshProUGUI.fontSize = fontSize;
                textMeshProUGUI.alignment = textAlignment switch
                {
                    TextAnchor.UpperLeft => TextAlignmentOptions.TopLeft,
                    TextAnchor.UpperCenter => TextAlignmentOptions.Top,
                    TextAnchor.UpperRight => TextAlignmentOptions.TopRight,
                    TextAnchor.MiddleLeft => TextAlignmentOptions.Left,
                    TextAnchor.MiddleCenter => TextAlignmentOptions.Center,
                    TextAnchor.MiddleRight => TextAlignmentOptions.Right,
                    TextAnchor.LowerLeft => TextAlignmentOptions.BottomLeft,
                    TextAnchor.LowerCenter => TextAlignmentOptions.Bottom,
                    TextAnchor.LowerRight => TextAlignmentOptions.BottomRight,
                    _ => throw new ArgumentOutOfRangeException()
                };

                EditorUtility.SetDirty(gameObjectWithText);
            }
        }

        public static void ConvertPrefabTextToTMP(GameObject prefab, TMP_FontAsset fontAsset)
        {
            AssetDatabaseUtils.ForEachPrefabRoot(
                prefab,
                prefabRoot => ScanGameObjectAndConvertTextToTMP(prefabRoot, fontAsset),
                true
            );
        }

        public static void ConvertAllProjectTextToTMP(TMP_FontAsset fontAsset)
        {
            // For Prefabs
            List<GameObject> prefabs = AssetDatabaseUtils.GetAllAssetsOfType<GameObject>();

            foreach (var prefab in prefabs)
            {
                if (!PrefabUtility.IsPartOfPrefabAsset(prefab))
                {
                    Debug.Log($"{prefab.name} is not a prefab asset.");
                    continue;
                }

                AssetDatabaseUtils.ForEachPrefabRoot(
                    prefab,
                    prefabRoot => ScanGameObjectAndConvertTextToTMP(prefabRoot, fontAsset),
                    true
                );
            }

            List<SceneAsset> sceneAssetList = AssetDatabaseUtils.GetAllAssetsOfType<SceneAsset>();

            foreach (var sceneAsset in sceneAssetList)
            {
                string assetPath = AssetDatabase.GetAssetPath(sceneAsset);
                Scene activeScene = EditorSceneManager.OpenScene(assetPath, OpenSceneMode.Single);
                var rootGameObjects = activeScene.GetRootGameObjects();

                foreach (var rootGameObject in rootGameObjects)
                {
                    ScanGameObjectAndConvertTextToTMP(rootGameObject, fontAsset);
                }
            }
        }
    }
}