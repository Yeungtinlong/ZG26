#if UNITY_EDITOR
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGame.ResourceManagement
{
    public class EditorResLoader : IResLoader
    {
        public UniTask<bool> Init() => UniTask.FromResult(true);

        public UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            return UniTask.FromResult(AssetDatabase.LoadAssetAtPath<T>($"Assets/_Resources/{path}"));
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>($"Assets/_Resources/{path}");
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode, bool activateOnLoad)
        {
            await EditorSceneManager.LoadSceneAsyncInPlayMode($"Assets/_Scenes/{sceneName}.unity", new LoadSceneParameters() { loadSceneMode = mode });
        }

        public void LoadScene(string sceneName, LoadSceneMode mode)
        {
            EditorSceneManager.LoadSceneInPlayMode($"Assets/_Scenes/{sceneName}.unity", new LoadSceneParameters() { loadSceneMode = mode });
        }
    }
}
#endif