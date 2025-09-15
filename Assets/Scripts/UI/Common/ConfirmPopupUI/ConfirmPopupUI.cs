using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class ConfirmPopupUI : BaseUI
    {
        public override UILayer Layer => UILayer.Popup;

        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _contentText;

        [SerializeField] private TMP_Text _confirmText;
        [SerializeField] private TMP_Text _cancelText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        private Action<ConfirmPopupUI, bool> _onConfirm;

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
            _confirmButton.onClick.AddListener(ConfirmButton_OnClick);
            _cancelButton.onClick.AddListener(CancelButton_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _confirmButton.onClick.RemoveListener(ConfirmButton_OnClick);
            _cancelButton.onClick.RemoveListener(CancelButton_OnClick);
        }

        private void CancelButton_OnClick()
        {
            _onConfirm?.Invoke(this, false);
        }

        private void ConfirmButton_OnClick()
        {
            _onConfirm?.Invoke(this, true);
        }

        public void Set(string title, string content, Action<ConfirmPopupUI, bool> onConfirm)
        {
            Set(title, content, "是", "否", onConfirm);
        }

        public void Set(string title, string content, string confirmText, string cancelText,
            Action<ConfirmPopupUI, bool> onConfirm)
        {
            _titleText.text = title;
            _contentText.text = content;
            _confirmText.text = confirmText;
            _cancelText.text = cancelText;
            _onConfirm = onConfirm;
        }
    }
}