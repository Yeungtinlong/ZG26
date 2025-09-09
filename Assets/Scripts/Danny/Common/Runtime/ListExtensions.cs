using System.Collections.Generic;
using UnityEngine;

namespace SupportUtils
{
    public static class ListExtensions
    {
        public static List<T> RandomPick<T>(this List<T> list, int count)
        {
            if (count <= 0)
                return new List<T>(0);

            List<int> unselectedIndexes = new List<int>();
            for (int i = 0; i < list.Count; i++)
                unselectedIndexes.Add(i);

            List<T> selected = new List<T>();
            int needCount = Mathf.Min(count, list.Count);
            while (selected.Count < needCount)
            {
                int rand = Random.Range(0, unselectedIndexes.Count);
                int selectedIndex = unselectedIndexes[rand];
                selected.Add(list[selectedIndex]);
                unselectedIndexes.RemoveAt(rand);
            }

            return selected;
        }

        public static T RandomPickOne<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        /// <summary>
        /// 水池算法
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomPickOne<T>(this IEnumerable<T> list)
        {
            using var enumerator = list.GetEnumerator();
            int i = 0;
            T result = default;
            while (enumerator.MoveNext())
            {
                // 以 1 / (i + 1) 选中，以 i / (i + 1) 保留
                if (Random.value <= 1 / (i + 1f))
                    result = enumerator.Current;

                i++;
            }

            return result;
        }
    }
}