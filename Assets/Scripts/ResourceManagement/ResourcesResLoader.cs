using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGame.ResourceManagement
{
    public class ResourcesResLoader : IResLoader
    {
        public UniTask<bool> Init()
        {
            return UniTask.FromResult(true);
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            return (T)await Resources.LoadAsync<T>(path);
        }

        public UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode, bool activateOnLoad)
        {
            return SceneManager.LoadSceneAsync(sceneName, mode).ToUniTask();
        }

        public void LoadScene(string sceneName, LoadSceneMode mode)
        {
            SceneManager.LoadScene(sceneName, mode);
        }
    }
}