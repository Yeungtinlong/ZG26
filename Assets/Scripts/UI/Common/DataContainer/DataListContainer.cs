using System.Collections.Generic;
using UnityEngine;

namespace TheGame.UI
{
    public abstract class DataListContainer<TBehaviour, TData> : BaseUI, IDataContainer<IList<TData>>
        where TBehaviour : MonoBehaviour, IDataContainer<TData>
    {
        [SerializeField] protected TBehaviour _itemPrefab;
        [SerializeField] protected Transform _itemContainer;

        protected readonly List<TBehaviour> _itemInstanceList = new List<TBehaviour>();

        public virtual void SetData(IList<TData> dataList)
        {
            if (_itemContainer.childCount != _itemInstanceList.Count)
            {
                _itemInstanceList.Clear();
                foreach (Transform item in _itemContainer)
                    if (item.TryGetComponent(out TBehaviour behaviour))
                        _itemInstanceList.Add(behaviour);
            }

            while (_itemInstanceList.Count < dataList.Count)
                _itemInstanceList.Add(Instantiate(_itemPrefab, _itemContainer));

            for (int i = dataList.Count; i < _itemInstanceList.Count; i++)
                _itemInstanceList[i].gameObject.SetActive(false);

            for (int i = 0; i < dataList.Count; i++)
            {
                _itemInstanceList[i].SetData(dataList[i]);
                _itemInstanceList[i].gameObject.SetActive(true);

                if (_itemInstanceList[i] is IClickableContainer<TBehaviour> clickableContainer)
                {
                    clickableContainer.OnClick -= ClickableContainer_OnClick;
                    clickableContainer.OnClick += ClickableContainer_OnClick;
                }

                if (_itemInstanceList[i] is IEventContainer<string> eventContainer)
                {
                    eventContainer.OnEventRaised -= EventContainer_OnEventRaised;
                    eventContainer.OnEventRaised += EventContainer_OnEventRaised;
                }
            }
        }

        protected virtual void EventContainer_OnEventRaised(object sender, string eventArgs) { }

        protected virtual void ClickableContainer_OnClick(TBehaviour clickableContainer) { }
    }
}