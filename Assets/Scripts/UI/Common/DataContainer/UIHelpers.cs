using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TheGame.UI
{
    public static class UIHelpers
    {
        public static void GenerateCachedListItems<TContainer, TData>(Transform parent, TContainer containerPrefab,
            List<TContainer> containerList, List<TData> dataList,
            Action<TContainer, TData> dataSetter) where TContainer : Component
        {
            if (parent.childCount != containerList.Count)
            {
                containerList.Clear();
                foreach (Transform item in parent)
                    if (item.TryGetComponent(out TContainer container))
                        containerList.Add(container);
            }

            while (containerList.Count < dataList.Count)
                containerList.Add(Object.Instantiate(containerPrefab, parent));

            for (int i = dataList.Count; i < containerList.Count; i++)
                containerList[i].gameObject.SetActive(false);

            for (int i = 0; i < dataList.Count; i++)
            {
                dataSetter.Invoke(containerList[i], dataList[i]);
                containerList[i].gameObject.SetActive(true);
            }
        }

        public static void GenerateCachedListItems<TContainer, TData>(Transform parent, TContainer containerPrefab,
            List<TContainer> containerList, List<TData> dataList,
            Action<int, TContainer, TData> dataSetter) where TContainer : Component
        {
            if (parent.childCount != containerList.Count)
            {
                containerList.Clear();
                foreach (Transform item in parent)
                    if (item.TryGetComponent(out TContainer container))
                        containerList.Add(container);
            }

            while (containerList.Count < dataList.Count)
                containerList.Add(Object.Instantiate(containerPrefab, parent));

            for (int i = dataList.Count; i < containerList.Count; i++)
                containerList[i].gameObject.SetActive(false);

            for (int i = 0; i < dataList.Count; i++)
            {
                dataSetter.Invoke(i, containerList[i], dataList[i]);
                containerList[i].gameObject.SetActive(true);
            }
        }
    }
}