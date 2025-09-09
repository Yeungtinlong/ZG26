using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _nextButton;

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
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void UnsubscribeFromEvents()
        {
            _exitButton.onClick.RemoveListener(ExitGame);
        }

        private void ExitGame()
        {
            TheGameSceneManager.Instance.ChangeScene("MainMenu");
        }
    }
}