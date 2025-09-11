using System;
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
            _exitButton.onClick.AddListener(ExitGame);
            _startGameButton.onClick.AddListener(StartGame);
        }

        private void UnsubscribeFromEvents()
        {
            _exitButton.onClick.RemoveListener(ExitGame);
            _startGameButton.onClick.RemoveListener(StartGame);
        }
        
        public void SetLevelText()
        {
            _levelText.gameObject.SetActive(true);
            _levelText.text = $"LEVEL: {GameRuntimeData.Instance.SelectedLevel}";
        }
        
        private void StartGame()
        {
            GameLuaInterface.game.StartGame();
            SetLevelText();
        }

        private void ExitGame()
        {
            TheGameSceneManager.Instance.ChangeScene("MainMenu");
        }
    }
}