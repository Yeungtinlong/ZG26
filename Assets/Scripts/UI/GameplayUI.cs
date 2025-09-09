using TheGame.GM;
using UnityEngine;

namespace TheGame.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private GameOverUI _gameOverUI;
        [SerializeField] private InGameUI _inGameUI;

        private void Awake()
        {
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.OnGameOver += OpenGameOverUI;
        }

        private void UnsubscribeFromEvents()
        {
            GameManager.OnGameOver -= OpenGameOverUI;
        }

        private void OpenGameOverUI()
        {
            _gameOverUI.gameObject.SetActive(true);
        }

        private void CloseGameOverUI()
        {
            _gameOverUI.gameObject.SetActive(false);
        }
    }
}