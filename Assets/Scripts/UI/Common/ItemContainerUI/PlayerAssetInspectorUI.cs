using TheGame.GM;
using UnityEngine;

namespace TheGame.UI
{
    public class PlayerAssetInspectorUI : MonoBehaviour
    {
        [SerializeField] private string _id;
        private ItemContainerUI _itemContainer;

        private int _currentCount = 0;

        private void Awake()
        {
            _itemContainer = GetComponent<ItemContainerUI>();
        }

        private void OnEnable()
        {
            _currentCount = GameRuntimeData.Instance.CountOfItem(_id);
            _itemContainer.Set($"Sprites/Items/ui_head_{_id}.png", _currentCount);
        }

        private void Update()
        {
            if (_currentCount != GameRuntimeData.Instance.CountOfItem(_id))
            {
                _currentCount = GameRuntimeData.Instance.CountOfItem(_id);
                _itemContainer.Set(_currentCount);
            }
        }
    }
}