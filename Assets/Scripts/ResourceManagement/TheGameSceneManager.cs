using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace TheGame.ResourceManagement
{
    public class TheGameSceneManager : MonoBehaviour
    {
        private static TheGameSceneManager _instance;

        public static TheGameSceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Object.FindAnyObjectByType<TheGameSceneManager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("TheGameSceneManager").AddComponent<TheGameSceneManager>();
                    }
                }

                return _instance;
            }
        }

        public static event Action<string, string> OnBeforeSceneTransition;
        public static event Action<string, float> OnSceneLoading;
        public static event Action<string> OnSceneTransitionComplete;

        public static event Action<string> OnBeforeSceneReady;

        private string _currentScene;
        private string _nextScene;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private async UniTask ChangeScene_Internal(string sceneName)
        {
            _currentScene = SceneManager.GetActiveScene().name;

            OnBeforeSceneTransition?.Invoke(_currentScene, sceneName);

            await ResLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);

            await SceneManager.UnloadSceneAsync(_currentScene).ToUniTask();

            OnBeforeSceneReady?.Invoke(sceneName);

            OnSceneTransitionComplete?.Invoke(sceneName);
        }
        
        public async UniTask ChangeScene(string sceneName)
        {
            await ChangeScene_Internal("EmptyScene");
            await ChangeScene_Internal(sceneName);
        }
    }
}