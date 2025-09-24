using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class NavigationMenuSelectorUI : MonoBehaviour
    {
        public NavigationMenuType NavigationMenuType => _navigationMenuType;
        
        [SerializeField] private Button _button;
        [SerializeField] private NavigationMenuType _navigationMenuType;
        [SerializeField] private GameObject _selectedObject;

        private Action<NavigationMenuSelectorUI> _onClick;
        private bool _selected;

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

        public void Set(bool selected, Action<NavigationMenuSelectorUI> onClick)
        {
            _onClick = onClick;
            _selectedObject.SetActive(selected);
            if (_selected != selected && selected)
            {
                transform.DOKill();
                transform.localScale = new Vector3(1f, 0.2f, 1f);
                transform.DOScaleY(1f, 0.2f).SetEase(Ease.OutBack);
            }
        }
    }
}