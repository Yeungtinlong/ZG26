using System;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class NavigationMenuSelectorUI : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private Action<NavigationMenuSelectorUI> _onClick;
        [SerializeField] private NavigationMenuType _navigationMenuType;
        public NavigationMenuType NavigationMenuType => _navigationMenuType;

        void OnEnable()
        {
            _button.onClick.AddListener(Button_OnClick);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(Button_OnClick);
        }

        private void Button_OnClick()
        {
            _onClick?.Invoke(this);
        }

        public void Set(Action<NavigationMenuSelectorUI> onClick)
        {
            _onClick = onClick;
        }
    }
}