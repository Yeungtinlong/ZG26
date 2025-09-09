using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using SupportUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheGame.ResourceManagement
{
    public class AssetBundleResLoader : IResLoader
    {
        private bool _isInit;
        private bool _useEncrypt = true;

        private AssetBundle _base;
        private AssetBundle _scenes;

        private const string _basePrefix = "Assets/_Resources/";
        private const string _scenesPrefix = "Assets/_Scenes/";
        
        public const string BUNDLE_PREFIX = "ambrctd";

        private readonly Dictionary<string, string> _assets = new Dictionary<string, string>();

        public async UniTask<bool> Init()
        {
            if (_isInit)
                return true;

            _isInit = true;

            if (_useEncrypt)
            {
                await using var s1 = new EncryptStream(Application.streamingAssetsPath + $"/{BUNDLE_PREFIX}_base", FileMode.Open,
                    FileAccess.Read, FileShare.None, 1024 * 4, true);
                _base = await AssetBundle.LoadFromStreamAsync(s1);
                await using var s2 =
                    new EncryptStream(Application.streamingAssetsPath + $"/{BUNDLE_PREFIX}_scenes", FileMode.Open,
                        FileAccess.Read, FileShare.None, 1024 * 4, true);
                _scenes = await AssetBundle.LoadFromStreamAsync(s2);
            }
            else
            {
                _base = await AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/base");
                _scenes = await AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/scenes");
            }

            _assets.Clear();
            foreach (var assetName in _base.GetAllAssetNames())
            {
                string assetNameLower = assetName.ToLower();
                string[] splits = assetNameLower.Split('.');
                _assets.Add(assetNameLower, assetNameLower);
                if (splits.Length == 2)
                    _assets.Add(splits[0], assetNameLower);
            }

            return true;
        }

        private string GetAssetPath(string path)
        {
            string key = $"{_basePrefix}{path}".ToLower();
            if (!_assets.ContainsKey(key))
                return null;

            return _assets[key];
        }

        public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            path = GetAssetPath(path);
            if (string.IsNullOrEmpty(path))
                return null;

            return (T)await _base.LoadAssetAsync<T>(path).ToUniTask();
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            path = GetAssetPath(path);
            if (string.IsNullOrEmpty(path))
                return null;

            return _base.LoadAsset<T>(path);
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode, bool activateOnLoad)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync($"{_scenesPrefix}{sceneName}", mode);
            op.allowSceneActivation = activateOnLoad;
            await op;
        }

        public void LoadScene(string sceneName, LoadSceneMode mode)
        {
            SceneManager.LoadScene($"{_scenesPrefix}{sceneName}", mode);
        }
    }
}