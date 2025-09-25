using System;
using System.Collections.Generic;
using TheGame.GM;
using TheGame.ResourceManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class ProductElementUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private ItemStacksInspectorUI _priceInspector;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _invalidObj;

        private Action<ProductElementUI> _onClick;
        public string ProductId { get; private set; }

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
            _button.onClick.AddListener(OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke(this);
        }

        public void Set(string productId, Action<ProductElementUI> onClick = null)
        {
            ProductId = productId;
            _onClick = onClick;

            LProductConfig productConfig = LuaToCsBridge.ShopTable[productId];

            _titleText.text = productConfig.Name;
            _iconImage.LoadAsyncForget(PathHelper.GetSpritePath(productConfig.Icon));
            _descriptionText.text = productConfig.Description;
            _priceInspector.Set(new List<ItemStack> { productConfig.Price });

            _invalidObj.SetActive(GameRuntimeData.Instance.CheckProductValid(ProductId));
        }
    }
}