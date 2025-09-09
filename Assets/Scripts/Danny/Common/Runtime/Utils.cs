using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SupportUtils
{
    public static class CommonExtensions
    {
        public static T DeepCopy<T>(this T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                ms.Position = 0;
                return (T) bf.Deserialize(ms);
            }
        }
    }

    public static class Utils
    {
        public static Transform FindFirstT(Transform t, Predicate<Transform> predicate)
        {
            if (predicate.Invoke(t))
                return t;
            foreach (Transform childT in t)
            {
                Transform result = FindFirstT(childT, predicate);
                if (result != null)
                    return result;
            }

            return null;
        }

        public static void FindAllTBFS<T>(T t, Predicate<T> predicate, List<T> results) where T : Transform
        {
            if (predicate.Invoke(t) && !results.Contains(t))
                results.Add(t);

            foreach (T childT in t)
                FindAllTBFS(childT, predicate, results);
        }

        public static List<T> FindAllTBFS<T>(T t, Predicate<T> predicate) where T : Transform
        {
            List<T> result = new List<T>();
            FindAllTBFS(t, predicate, result);
            return result;
        }

        public static void FindAllTDFS<T>(T t, Predicate<T> predicate, List<T> results) where T : Transform
        {
            foreach (T childT in t)
                FindAllTDFS(childT, predicate, results);

            if (predicate.Invoke(t) && !results.Contains(t))
                results.Add(t);
        }

        public static List<T> FindAllTDFS<T>(T t, Predicate<T> predicate) where T : Transform
        {
            List<T> result = new List<T>();
            FindAllTDFS(t, predicate, result);
            return result;
        }

        public static KeyValuePair<TKey, TValue> GetRandomPair<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            int index = Random.Range(0, dictionary.Count);
            int current = 0;
            foreach (var pair in dictionary)
            {
                if (current == index)
                    return pair;

                current++;
            }

            return default;
        }
    }
}