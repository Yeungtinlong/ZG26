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
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitButton;

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
            _startGameButton.onClick.AddListener(StartGame_OnClick);
            _exitButton.onClick.AddListener(ExitGame_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _startGameButton.onClick.RemoveListener(StartGame_OnClick);
            _exitButton.onClick.RemoveListener(ExitGame_OnClick);
        }
        
        public void SetLevelText()
        {
            _levelText.gameObject.SetActive(true);
            _levelText.text = $"LEVEL: {GameRuntimeData.Instance.SelectedLevel}";
        }
        
        private void StartGame_OnClick()
        {
            GameLuaInterface.game.StartGame();
            SetLevelText();
        }

        private void ExitGame_OnClick()
        {
            TheGameSceneManager.Instance.ChangeScene("MainMenu");
        }
    }
}