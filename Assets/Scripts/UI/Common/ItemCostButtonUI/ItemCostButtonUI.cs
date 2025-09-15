using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class ItemCostButtonUI : MonoBehaviour
    {
        [SerializeField] private Button _clickable;

        [SerializeField] private Image _bgImage;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        [SerializeField] private ItemStackUI _prefab;
        [SerializeField] private Transform _containerParent;
        private readonly List<ItemStackUI> _itemStacks = new List<ItemStackUI>();

        private Action<ItemCostButtonUI> _onClick;

        private void Awake()
        {
            _clickable.onClick.AddListener(() => _onClick?.Invoke(this));
        }

        public void SetActive(bool active)
        {
            _bgImage.sprite = active ? _activeSprite : _inactiveSprite;
        }

        public void Set(List<ItemStack> data, Action<ItemCostButtonUI> onClick = null)
        {
            _onClick = onClick;
            UIHelpers.GenerateCachedListItems(_containerParent, _prefab, _itemStacks, data,
                (ui, stack) => ui.Set(stack));
        }
    }
}