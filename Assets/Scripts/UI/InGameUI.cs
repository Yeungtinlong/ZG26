using TheGame.GM;
using TheGame.ResourceManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private GameObject _readyPanel;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            SubscribeToEvents();
            SetLevelText();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _startGameButton.onClick.AddListener(StartGame_OnClick);
            _exitButton.onClick.AddListener(ExitGame_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _startGameButton.onClick.RemoveListener(StartGame_OnClick);
            _exitButton.onClick.RemoveListener(ExitGame_OnClick);
        }
        
        private void SetLevelText()
        {
            _levelText.gameObject.SetActive(true);
            _levelText.text = $"LEVEL: {GameRuntimeData.Instance.SelectedLevel}";
        }
        
        private void StartGame_OnClick()
        {
            GameLuaInterface.game.StartGame();
            _readyPanel.SetActive(false);
        }

        private void ExitGame_OnClick()
        {
            TheGameSceneManager.Instance.ChangeScene("MainMenu");
            _readyPanel.SetActive(false);
        }
    }
}