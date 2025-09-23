using TheGame.GM;
using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class GameOverPanelUI : MonoBehaviour
    {
        [Header("Part of shared")] [SerializeField]
        private Button _nextButton;

        [SerializeField] private Button _exitButton;

        [Header("Part of win")] [SerializeField]
        private ItemStacksInspectorUI _rewardsInspector;

        [SerializeField] private GameObject _winPart;

        [Header("Part of lose")] [SerializeField]
        private Button _restartButton;
        [SerializeField] private GameObject _losePart;

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
            _exitButton.onClick.AddListener(ExitButton_OnClick);
            _nextButton.onClick.AddListener(NextButton_OnClick);
            _restartButton.onClick.AddListener(RestartButton_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _exitButton.onClick.RemoveListener(ExitButton_OnClick);
            _nextButton.onClick.RemoveListener(NextButton_OnClick);
            _restartButton.onClick.RemoveListener(RestartButton_OnClick);
        }

        private void RestartButton_OnClick()
        {
            TheGameSceneManager.Instance.ChangeScene("Gameplay");
        }

        private void NextButton_OnClick()
        {
            GameRuntimeData.Instance.SelectedLevel = GameRuntimeData.Instance.PassedLevel + 1;
            TheGameSceneManager.Instance.ChangeScene("Gameplay");
        }

        public void Set(GameResult gameResult, int level)
        {
            _winPart.SetActive(gameResult is GameResult.Win or GameResult.NewWin);
            _losePart.SetActive(gameResult == GameResult.Lose);
            
            if (gameResult is GameResult.NewWin && GameRuntimeData.Instance.SelectedLevel == GameRuntimeData.Instance.PassedLevel)
            {
                _rewardsInspector.gameObject.SetActive(true);
                _rewardsInspector.Set(LuaToCsBridge.LevelTable[level].rewards);
            }
            else
            {
                _rewardsInspector.gameObject.SetActive(false);
            }
        }
        
        private void ExitButton_OnClick()
        {
            TheGameSceneManager.Instance.ChangeScene("MainMenu");
        }
    }
}