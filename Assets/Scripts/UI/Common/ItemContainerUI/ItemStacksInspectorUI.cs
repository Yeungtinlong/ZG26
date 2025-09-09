using System.Collections.Generic;
using TheGame.UI;
using UnityEngine;

namespace TheGame.UI
{
    public class ItemStacksInspectorUI : MonoBehaviour
    {
        [SerializeField] private ItemStackUI _prefab;
        [SerializeField] private Transform _containerParent;
        private readonly List<ItemStackUI> _itemStacks = new List<ItemStackUI>();
        public List<ItemStackUI> ItemStacks => _itemStacks;

        public void Set(List<ItemStack> data)
        {
            UIHelpers.GenerateCachedListItems(_containerParent, _prefab, _itemStacks, data, (ui, stack) => ui.Set(stack));
        }
    }
}