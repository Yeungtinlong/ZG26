using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Game.Core.ResourceSystem
{
    public class AssetBundleAddressablesRemoteResLoader : IResLoader
    {
        private string _baseUrl = "base_assets_all.bundle";
        private string _scenesUrl = "build_scenes_all.bundle";

#if UNITY_WEBGL
        private string _platform = "WebGL";
#elif UNITY_IOS
        private string _platform = "iOS";
#elif UNITY_ANDROID
        private string _platform = "Android";
#endif

        private string _rootUrl => $"{Application.streamingAssetsPath}/aa/{_platform}";

        private bool _isInit;

        private AssetBundle _base;
        private AssetBundle _scenes;

        public async UniTask<bool> Init()
        {
            if (_isInit)
                return true;

            _isInit = true;

            _base = await DownloadAssetBundle($"{_rootUrl}/{_baseUrl}");
            _scenes = await DownloadAssetBundle($"{_rootUrl}/{_scenesUrl}");
            
            return true;
        }

        private async UniTask<AssetBundle> DownloadAssetBundle(string url)
        {
            using var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            await request.SendWebRequest();

            int retryTimes = 0;

            while (request.result != UnityWebRequest.Result.Success)
            {
                if (retryTimes > 20)
                {
                    Debug.LogError($"下载失败，请检查网络状态。");
                    return null;
                }

                retryTimes++;
                await UniTask.Delay(20);
                Debug.LogError($"下载失败，重试 {retryTimes} 次");
                await request.SendWebRequest();
            }

            return ((DownloadHandlerAssetBundle)request.downloadHandler).assetBundle;
        }

        public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            return (T)await _base.LoadAssetAsync<T>(path);
        }

        public UniTask<Object> LoadAssetAsync(string path)
        {
            return LoadAssetAsync<Object>(path);
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            return _base.LoadAsset<T>(path);
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
    }
}