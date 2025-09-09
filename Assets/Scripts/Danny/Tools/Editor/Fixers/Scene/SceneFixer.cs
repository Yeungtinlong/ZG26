using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SupportUtils
{
    public class SceneFixerWindow : OdinEditorWindow
    {
        [MenuItem("Danny/Fixers/Scene Fixer")]
        static void OpenWindow()
        {
            GetWindow<SceneFixerWindow>().Show();
        }

        [BoxGroup("LightSettings", LabelText = "Remove All Scenes LightSettings")]
        [HorizontalGroup("LightSettings/Horizontal")]
        [SerializeField]
        private List<SceneAsset> _sceneList;

        [BoxGroup("LightSettings")]
        [HorizontalGroup("LightSettings/Horizontal", width: 50)]
        [Button("FIX")]
        private void RemoveAllScenesLightSettings()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            string path = activeScene.path;
            
            foreach (var sceneAsset in _sceneList)
            {
                Scene openedScene = EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(sceneAsset));

                Lightmapping.lightingSettings = null;
                RenderSettings.skybox = null;

                EditorSceneManager.SaveScene(openedScene);
            }

            EditorSceneManager.OpenScene(path);
        }
    }
}