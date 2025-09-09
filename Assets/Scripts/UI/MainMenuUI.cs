using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

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
            _startButton.onClick.AddListener(StartGame);
        }

        private void UnsubscribeFromEvents()
        {
            _startButton.onClick.RemoveListener(StartGame);
        }
        
        private void StartGame()
        {
            TheGameSceneManager.Instance.ChangeScene("Gameplay");
        }
    }
}