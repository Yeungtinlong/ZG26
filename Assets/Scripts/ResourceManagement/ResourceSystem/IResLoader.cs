using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Core.ResourceSystem
{
    public interface IResLoader
    {
        UniTask<bool> Init();
        UniTask<T> LoadAssetAsync<T>(string path) where T : Object;
        UniTask<Object> LoadAssetAsync(string path);
        T LoadAsset<T>(string path) where T : Object;
        Object LoadAsset(string path);
        UniTask LoadSceneAsync(string sceneName);
        void LoadScene(string sceneName);
    }
}