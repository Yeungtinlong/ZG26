using Cysharp.Threading.Tasks;
using DG.Tweening;
using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private Camera _camera;

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            TheGameSceneManager.OnBeforeSceneTransition += OnSceneBeforeLoad;
            TheGameSceneManager.OnSceneTransitionComplete += OnSceneAfterLoaded;
        }

        private void UnsubscribeFromEvents()
        {
            TheGameSceneManager.OnBeforeSceneTransition -= OnSceneBeforeLoad;
            TheGameSceneManager.OnSceneTransitionComplete -= OnSceneAfterLoaded;
        }

        private void OnSceneBeforeLoad(string currentScene, string sceneName)
        {
            _fadeImage.color = new Color(0, 0, 0, 1f);
            _fadeImage.gameObject.SetActive(true);
            _camera.gameObject.SetActive(true);
        }

        private async void OnSceneAfterLoaded(string sceneName)
        {
            await FadeIn(1f);
            _fadeImage.gameObject.SetActive(false);
            _camera.gameObject.SetActive(false);
        }

        public async UniTask<LoadingScreen> FadeIn(float duration)
        {
            _fadeImage.color = Color.black;
            await _fadeImage.DOFade(0f, duration).SetEase(Ease.Linear).SetUpdate(true);
            return this;
        }

        public async UniTask<LoadingScreen> FadeOut(float duration)
        {
            _fadeImage.color = Color.clear;
            await _fadeImage.DOFade(1f, duration).SetEase(Ease.Linear).SetUpdate(true);
            return this;
        }
    }
}