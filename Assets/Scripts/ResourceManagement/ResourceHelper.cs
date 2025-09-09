using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.ResourceManagement
{
    public static class ResourceHelper
    {
        public static async UniTask LoadAsync(this Image image, string spritePath)
        {
            if (image.sprite != null && spritePath.Contains(image.sprite.name)) 
                return;
            
            image.gameObject.SetActive(false);
            image.sprite = await ResLoader.LoadAssetAsync<Sprite>(spritePath);
            image.gameObject.SetActive(true);
        }
        
        public static void LoadAsyncForget(this Image image, string spritePath)
        {
            image.LoadAsync(spritePath).Forget();
        }
    }
}