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
        [SerializeField] private Button _pauseButton;

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
            _pauseButton.onClick.AddListener(PauseButton_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _startGameButton.onClick.RemoveListener(StartGame_OnClick);
            _exitButton.onClick.RemoveListener(ExitGame_OnClick);
            _pauseButton.onClick.RemoveListener(PauseButton_OnClick);
        }

        private void PauseButton_OnClick()
        {
            UIManager.Instance.OpenUI<ConfirmPopupUI>()
                .Set("暂停游戏",
                    "你确定要退出对局吗？",
                    "全军撤退",
                    "返回战斗",
                    (popup, confirm) =>
                    {
                        UIManager.Instance.CloseUI(popup);
                        if (confirm)
                            ExitGame_OnClick();
                        else
                            GameLuaInterface.game.SetPause(false);
                    });

            GameLuaInterface.game.SetPause(true);
        }

        private void SetLevelText()
        {
            _levelText.gameObject.SetActive(true);
            _levelText.text = $"LEVEL: {GameRuntimeData.Instance.SelectedLevel}";
        }

        private void StartGame_OnClick()
        {
            if (GameLuaInterface.game.StartGame())
                _readyPanel.SetActive(false);
        }

        private void ExitGame_OnClick()
        {
            TheGameSceneManager.Instance.ChangeScene("MainMenu");
            _readyPanel.SetActive(false);
        }
    }
}