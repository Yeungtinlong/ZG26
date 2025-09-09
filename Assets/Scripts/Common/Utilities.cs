using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public static class Utilities
    {
        public static int Sqr(this int number) => number * number;
        public static float Sqr(this float number) => number * number;

        public static bool ContainsTagNotNull(this string[] tags, string tag)
        {
            return tags != null && tags.Contains(tag);
        }

        public static void RemoveNullElements<T>(this List<T> list) where T : class
        {
            int writeIdx = 0;
            for (int readIdx = 0; readIdx < list.Count; readIdx++)
            {
                if (list[readIdx] != null)
                    list[writeIdx++] = list[readIdx];
            }

            list.RemoveRange(writeIdx, list.Count - writeIdx);
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent(out T component))
                return component;
            return gameObject.AddComponent<T>();
        }

        public static T GetOrAddComponent<T>(this Component com) where T : Component
        {
            return GetOrAddComponent<T>(com.gameObject);
        }

        public static void TyperEffect(this TMP_Text tmpText, float visibleInterval, ScrollRect scrollRect, Action onComplete = null)
        {
            tmpText.StopAllCoroutines();
            tmpText.StartCoroutine(ShowStory());

            IEnumerator ShowStory()
            {
                tmpText.maxVisibleCharacters = 0;
                while (tmpText.maxVisibleCharacters <= tmpText.text.Length)
                {
                    tmpText.maxVisibleCharacters++;
                    int lastVisibleLine = 0;
                    for (int i = 0; i < tmpText.textInfo.characterCount; i++)
                    {
                        if (tmpText.textInfo.characterInfo[i].isVisible)
                        {
                            lastVisibleLine = tmpText.textInfo.characterInfo[i].lineNumber;
                        }
                    }

                    float currentHeight = (lastVisibleLine + 1) * tmpText.textInfo.lineInfo[0].lineHeight;
                    if (scrollRect != null)
                    {
                        scrollRect.content.sizeDelta = new Vector2(0f, currentHeight);
                        scrollRect.verticalNormalizedPosition = 0f;
                    }

                    yield return new WaitForSeconds(visibleInterval);
                }

                if (scrollRect != null)
                    scrollRect.vertical = true;

                onComplete?.Invoke();
            }
        }
    }
}