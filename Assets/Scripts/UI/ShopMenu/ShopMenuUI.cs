using System;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class ShopMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        private Action _onClose;
        
        private void OnEnable()
        {
            SubscribeToEvents();
            RefreshUI();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _closeButton.onClick.AddListener(Close_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _closeButton.onClick.RemoveListener(Close_OnClick);
        }

        public void Set(Action onClose)
        {
            _onClose = onClose;
        }
        
        private void Close_OnClick()
        {
            _onClose?.Invoke();
        }

        private void RefreshUI()
        {
            
        }
    }
}