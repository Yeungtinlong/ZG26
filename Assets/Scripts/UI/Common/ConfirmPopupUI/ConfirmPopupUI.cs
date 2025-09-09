using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class ConfirmPopupUI : BaseUI
    {
        public override UILayer Layer => UILayer.Popup;

        private bool? _confirmed = null;

        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        private Action<bool> _callback;

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(ConfirmButton_OnClick);
            _cancelButton.onClick.AddListener(CancelButton_OnClick);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(ConfirmButton_OnClick);
            _cancelButton.onClick.RemoveListener(CancelButton_OnClick);
        }

        private void CancelButton_OnClick()
        {
            _confirmed = false;
            _callback?.Invoke(_confirmed.Value);
        }

        private void ConfirmButton_OnClick()
        {
            _confirmed = true;
            _callback?.Invoke(_confirmed.Value);
        }

        public async UniTask<bool> ShowPopup()
        {
            while (!_confirmed.HasValue)
                await UniTask.NextFrame();

            this.uiManager.CloseUI(this);
            return _confirmed.Value;
        }

        public void ShowPopup(Action<bool> onComplete)
        {
            _callback = onComplete;
        }
    }
}