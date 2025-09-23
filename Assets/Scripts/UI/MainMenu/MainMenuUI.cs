using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace TheGame.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private List<INavigationMenu> _navigationMenus;
        private List<NavigationMenuSelectorUI> _navigationMenuSelectors;

        private void Awake()
        {
            _navigationMenus = GetComponentsInChildren<INavigationMenu>(true).ToList();
            _navigationMenuSelectors = GetComponentsInChildren<NavigationMenuSelectorUI>(true).ToList();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
            SetDefaultMenu();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            foreach (var selector in _navigationMenuSelectors)
            {
                selector.Set(NavigationMenuSelector_OnClick);
            }
        }

        private void UnsubscribeFromEvents()
        {
        }

        private void NavigationMenuSelector_OnClick(NavigationMenuSelectorUI currentSelector)
        {
            Set(currentSelector.NavigationMenuType);
        }

        private void Set(NavigationMenuType navigationMenuType)
        {
            foreach (var menu in _navigationMenus)
            {
                if (navigationMenuType == menu.Type)
                {
                    (menu as Component)?.gameObject.SetActive(true);
                    menu.Set();
                }
                else
                {
                    (menu as Component)?.gameObject.SetActive(false);
                }
            }

            foreach (var selector in _navigationMenuSelectors)
            {
                selector.DOKill();
                selector.transform.DOLocalMoveY(selector.NavigationMenuType == navigationMenuType ? 40f : 0f, 0.2f);
                selector.transform.DOScale(selector.NavigationMenuType == navigationMenuType
                    ? Vector3.one * 1.2f
                    : Vector3.one, 0.2f);
            }
        }

        private void SetDefaultMenu()
        {
            Set(NavigationMenuType.Level);
        }
    }
}