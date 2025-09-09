using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TheGame.ResourceManagement
{
    public interface IResLoader
    {
        public UniTask<bool> Init();
        public T LoadAsset<T>(string path) where T : UnityEngine.Object;
        public UniTask<T> LoadAssetAsync<T>(string path) where T : UnityEngine.Object;
        public UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode, bool activateOnLoad);
        public void LoadScene(string sceneName, LoadSceneMode mode);
    }
}