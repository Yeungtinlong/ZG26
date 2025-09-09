using System;
using UnityEngine;

namespace TheGame.UI
{
    public abstract class BaseUI : MonoBehaviour
    {
        public UIManager uiManager { get; set; }

        public event Action<BaseUI> OnCloseEvent;
        private bool _isClosing = false;

        public abstract UILayer Layer { get; }

        public virtual void OnOpen() { }
        public virtual void OnOpened() { }

        public virtual void OnClose()
        {
            if (_isClosing)
                return;
            
            _isClosing = true;
            OnCloseEvent?.Invoke(this);
        }
        
        public virtual void OnClosed() {}
    }
}