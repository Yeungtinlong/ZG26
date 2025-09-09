using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core.ResourceSystem
{
    public class ResourcesResLoader : IResLoader
    {
        public UniTask<bool> Init() => UniTask.FromResult(true);

        public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            return (T) await Resources.LoadAsync<T>(ProjectPathFromAssetPathToResourcesPath(path)).ToUniTask();
        }

        public UniTask<Object> LoadAssetAsync(string path)
        {
            return LoadAssetAsync<Object>(path);
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(ProjectPathFromAssetPathToResourcesPath(path));
        }

        public Object LoadAsset(string path)
        {
            return LoadAsset<Object>(path);
        }

        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        private string ProjectPathFromAssetPathToResourcesPath(string assetPath)
        {
            return assetPath.TrimStart("Assets/".ToCharArray());
        }
    }
}