using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGame.ResourceManagement
{
    public class TheGameSceneResources : MonoBehaviour
    {
        private static Dictionary<string, string[]> _dependencies = new Dictionary<string, string[]>()
        {
            { "MainMenu", new string[] { "MainMenuUI" } },
            { "Gameplay", new string[] { "GameplayUI" } },
        };

        private string _parentScene;

        private void Awake()
        {
            _parentScene = gameObject.scene.name;

            TheGameSceneManager.OnBeforeSceneTransition += CheckUnloadDependencies;
            TheGameSceneManager.OnBeforeSceneReady += CheckLoadDependencies;
        }

        private void OnDestroy()
        {
            TheGameSceneManager.OnBeforeSceneTransition -= CheckUnloadDependencies;
            TheGameSceneManager.OnBeforeSceneReady -= CheckLoadDependencies;
        }

        private void CheckLoadDependencies(string nextScene)
        {
            if (nextScene == _parentScene)
            {
                LoadDependencies();
            }
        }

        private void CheckUnloadDependencies(string currentScene, string nextScene)
        {
            if (currentScene == _parentScene)
            {
                UnloadDependencies();
            }
        }

        private async void LoadDependencies()
        {
            if (_dependencies.ContainsKey(_parentScene))
            {
                foreach (var sceneName in _dependencies[_parentScene])
                {
                    // 1. Check if the scene has been loaded.
                    Scene scene = SceneManager.GetSceneByName(sceneName);
                    if (scene.isLoaded)
                    {
                        continue;
                    }

                    await ResLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive, false);
                }
            }
        }

        private void UnloadDependencies()
        {
            if (_dependencies.ContainsKey(_parentScene))
            {
                foreach (var dependedSceneName in _dependencies[_parentScene])
                {
                    SceneManager.UnloadSceneAsync(dependedSceneName);
                }
            }
        }
    }
}