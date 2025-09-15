using TheGame.GM;
using UnityEngine;

namespace TheGame.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private GameOverPanelUI _gameOverPanelUI;
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
            GameManager.OnGameOver += GameManager_OnGameOver;
        }

        private void UnsubscribeFromEvents()
        {
            GameManager.OnGameOver -= GameManager_OnGameOver;
        }

        private void GameManager_OnGameOver(bool win, int level)
        {
            _gameOverPanelUI.gameObject.SetActive(true);
            _gameOverPanelUI.Set(win, level);
        }
    }
}