using System;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class LoadingCanvas : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _startButton;

        public event Action OnStartButtonClicked;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartButton_OnClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartButton_OnClick);
        }

        private void StartButton_OnClick()
        {
            _startButton.gameObject.SetActive(false);
            OnStartButtonClicked?.Invoke();
        }

        public void ShowStartButton()
        {
            _startButton.gameObject.SetActive(true);
        }
        
        public void SetProgress(float value)
        {
            _slider.value = value;
        }
    }
}