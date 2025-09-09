using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SupportUtils
{
    public static class MonoExtensions
    {
        private const float MENU_FADE_DURATION = 0.2f;

        public static void SetCanvasGroupInteractable(this CanvasGroup canvasGroup, bool isActive)
        {
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }

        public static void SetCanvasGroupActive(this CanvasGroup canvasGroup, bool isActive)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.SetCanvasGroupInteractable(isActive);
        }

        public static void SetIconToFitContainer(this Image imageComponent, Sprite spriteToSet,
            RectTransform containerToFit)
        {
            imageComponent.sprite = spriteToSet;
            imageComponent.SetNativeSize();
            RectTransform mainRt = imageComponent.GetComponent<RectTransform>();
            Rect mainRect = mainRt.rect;
            bool isWidth = mainRect.width > mainRect.height;

            if (isWidth)
            {
                float rate = containerToFit.rect.width / mainRect.width;
                mainRt.sizeDelta = new Vector2(containerToFit.rect.width, mainRect.height * rate);
            }
            else
            {
                float rate = containerToFit.rect.height / mainRect.height;
                mainRt.sizeDelta = new Vector2(mainRect.width * rate, containerToFit.rect.height);
            }

            mainRt.anchorMin = new Vector2(0.5f, 0.5f);
            mainRt.anchorMax = mainRt.anchorMin;
            mainRt.anchoredPosition = Vector2.zero;
        }

        public static Texture2D CopyData(this Texture2D origin)
        {
            Texture2D texture2DCopy = new Texture2D(origin.width, origin.height, origin.format, false);
            texture2DCopy.name = origin.name;
            texture2DCopy.LoadRawTextureData(origin.GetRawTextureData());
            texture2DCopy.Apply();
            return texture2DCopy;
        }

        public static void ForceRecalculateLayouts(this RectTransform rectTransform)
        {
            List<RectTransform> rts = Utils.FindAllTDFS(rectTransform, r => true);
            rts.ForEach(LayoutRebuilder.ForceRebuildLayoutImmediate);
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (!go.TryGetComponent(out T result))
                result = go.AddComponent<T>();
            return result;
        }

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetOrAddComponent<T>();
        }
    }
}