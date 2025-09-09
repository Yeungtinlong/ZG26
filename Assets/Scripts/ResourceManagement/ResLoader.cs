using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace TheGame.ResourceManagement
{
    public static class ResLoader
    {
        private static bool _isTestingAssetBundle = false;

        private static readonly IResLoader _resLoader = new ResourcesResLoader();

//         static ResLoader()
//         {
// #if UNITY_EDITOR
//             _resLoader = _isTestingAssetBundle ? new AssetBundleResLoader() : new EditorResLoader();
// #else
//             _resLoader = new AssetBundleResLoader();
// #endif
//         }

        public static UniTask<bool> Init()
        {
            return _resLoader.Init();
        }
        
        public static T LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            T asset = _resLoader.LoadAsset<T>(path);
            if (asset == null)
            {
                string[] splits = path.Split('.');
                if (splits.Length == 2)
                    asset = _resLoader.LoadAsset<T>(splits[0]);
            }
            return asset;
        }

        public static async UniTask<T> LoadAssetAsync<T>(string path) where T : UnityEngine.Object
        {
            T asset = await _resLoader.LoadAssetAsync<T>(path);
            if (asset == null)
            {
                string[] splits = path.Split('.');
                if (splits.Length == 2)
                    asset = await _resLoader.LoadAssetAsync<T>(splits[0]);
            }

            return asset;
        }

        public static UniTask LoadSceneAsync(string path, LoadSceneMode mode = LoadSceneMode.Single, bool activateOnLoad = false)
        {
            return _resLoader.LoadSceneAsync(path, mode, activateOnLoad);
        }

        public static void LoadScene(string path, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _resLoader.LoadScene(path, mode);
        }
    }
}