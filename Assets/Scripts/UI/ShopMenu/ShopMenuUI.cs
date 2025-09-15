using System;
using System.Collections.Generic;
using System.Linq;
using TheGame.GM;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class ShopMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private ProductElementUI _productElementPrefab;
        [SerializeField] private Transform _container;
        private readonly List<ProductElementUI> _productElementUis = new List<ProductElementUI>();
        
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
            UIHelpers.GenerateCachedListItems(
                _container,
                _productElementPrefab, 
                _productElementUis,
                
                LuaToCsBridge.ShopTable.Values.ToList(),
                (ele, data) =>
            {
                ele.Set(data.Id, Product_OnClick);
            });
        }

        private void Product_OnClick(ProductElementUI element)
        {
            LProductConfig productConfig = LuaToCsBridge.ShopTable[element.ProductId];
            UIManager.Instance.OpenUI<ConfirmPopupUI>().Set("购买",$"确认花费{productConfig.Price.count}，购买{productConfig.Name}？", (popup,isConfirm) =>
            {
                UIManager.Instance.CloseUI(popup);
                if (isConfirm)
                {
                    if (!GameRuntimeData.Instance.Purchase(productConfig.Id))
                    {
                        UIManager.Instance.OpenUI<MessagePopupUI>().Set("金币不足", 1f);
                        return;
                    }
                    
                    UIManager.Instance.OpenUI<MessagePopupUI>().Set($"获得{productConfig.Name}", 1f);
                    RefreshUI();
                    GameRuntimeData.SaveGame();
                }
            });
        }
    }
}