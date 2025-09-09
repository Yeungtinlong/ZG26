using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace SupportUtils
{
    public static class UIAnimationExtensions
    {
        /// <summary>
        /// The base method to implement the menu background fading effect.
        /// </summary>
        /// <param name="menuBg">The background you want to fade.</param>
        /// <param name="alphaFrom"></param>
        /// <param name="alphaTo"></param>
        /// <param name="duration"></param>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public static TweenerCore<Color, Color, ColorOptions> MenuBgFade(this Image menuBg, float alphaFrom,
            float alphaTo, float duration,
            Action onCompleted = null)
        {
            menuBg.DOKill();
            menuBg.color = new Color(0f, 0f, 0f, alphaFrom);
            return menuBg.DOFade(alphaTo, duration).OnComplete(() => onCompleted?.Invoke());
        }

        public static TweenerCore<Color, Color, ColorOptions> MenuBgFadeIn(this Image menuBg, Action onCompleted = null)
            => MenuBgFade(menuBg, 0f, 0.5f, 0.25f, onCompleted);

        public static TweenerCore<Color, Color, ColorOptions> MenuBgFadeOut(this Image menuBg,
            Action onCompleted = null)
            => MenuBgFade(menuBg, 0.5f, 0f, 0.125f, onCompleted);


        /// <summary>
        /// The base method to implement the popup effect.
        /// </summary>
        /// <param name="popupContent"></param>
        /// <param name="startScale"></param>
        /// <param name="targetScale"></param>
        /// <param name="duration"></param>
        /// <param name="ease"></param>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> PopupScale(this Transform popupContent,
            float startScale, float targetScale, float duration,
            Ease ease = Ease.InOutSine, Action onCompleted = null)
        {
            popupContent.DOKill();
            popupContent.localScale = startScale * Vector3.one;
            return popupContent.DOScale(targetScale, duration).SetEase(ease).OnComplete(() => onCompleted?.Invoke());
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> PopupOpen(this Transform popupContent,
            Action onCompleted = null)
            => PopupScale(popupContent, 0.01f, 1f, 0.25f, Ease.OutBack, onCompleted);

        public static TweenerCore<Vector3, Vector3, VectorOptions> PopupClose(this Transform popupContent,
            Action onCompleted = null)
            => PopupScale(popupContent, 1f, 0.01f, 0.125f, Ease.InOutSine, onCompleted);

        /// <summary>
        /// The base method to implement the menu stretch effect.
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="fromScale"></param>
        /// <param name="toScale"></param>
        /// <param name="duration"></param>
        /// <param name="ease"></param>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> MenuStretch(this RectTransform menu,
            Vector3 fromScale, Vector3 toScale, float duration, Ease ease = Ease.OutBack, Action onCompleted = null)
        {
            menu.DOKill();
            Vector2 originPivot = menu.pivot;
            menu.pivot = new Vector2(0f, 0.5f);
            menu.localScale = fromScale;

            return menu.DOScale(toScale, duration).SetEase(ease).OnComplete(() =>
            {
                menu.pivot = originPivot;
                onCompleted?.Invoke();
            });
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> MenuStretchOpen(this RectTransform menu,
            Action onCompleted = null)
            => MenuStretch(menu, new Vector3(0f, 1f, 1f), Vector3.one, 0.25f, Ease.OutBack, onCompleted);

        public static TweenerCore<Vector3, Vector3, VectorOptions> MenuStretchClose(this RectTransform menu,
            Action onCompleted = null)
            => MenuStretch(menu, Vector3.one, new Vector3(0f, 1f, 1f), 0.25f, Ease.InBack, onCompleted);

        /// <summary>
        /// Infinitely loop rotate a transform.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="axis"></param>
        /// <param name="angleSpeed"></param>
        public static void LoopRotate(this Transform t, Vector3 axis, float angleSpeed)
        {
            t.DOKill();
            t.rotation = Quaternion.identity;
            t.DORotate(axis * angleSpeed, 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }

        /// <summary>
        /// The base method to change rectTransform pivot to implement the drop or raise effect.
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="fromPivot"></param>
        /// <param name="toPivot"></param>
        /// <param name="duration"></param>
        /// <param name="ease"></param>
        /// <param name="onCompleted"></param>
        private static void MenuPivotMove(this RectTransform rt, RectTransform parentRt, Vector2 fromPivot,
            Vector2 toPivot, Vector2 parentPivot, float duration,
            Ease ease, Action onCompleted = null)
        {
            Vector2 originParentPivot = parentRt.pivot;
            Vector2 originPivot = rt.pivot;

            rt.ChangePivot(fromPivot);
            parentRt.ChangePivot(parentPivot);
            rt.localPosition = Vector3.zero;

            rt.ChangePivot(toPivot);

            rt.DOLocalMove(fromPivot, duration).SetEase(ease).OnComplete(() =>
            {
                rt.ChangePivot(originPivot);
                parentRt.ChangePivot(originParentPivot);
                onCompleted?.Invoke();
            });
        }

        public static void MenuRaise(this RectTransform rt, RectTransform parentRt, float duration,
            Ease ease = Ease.InBack,
            Action onCompleted = null)
            => MenuPivotMove(rt, parentRt, new Vector2(0.5f, 1f), new Vector2(0.5f, 0f), new Vector2(0.5f, 1f),
                duration, ease, onCompleted);

        public static void MenuDrop(this RectTransform rt, RectTransform parentRt, float duration,
            Ease ease = Ease.InBack,
            Action onCompleted = null)
            => MenuPivotMove(rt, parentRt, new Vector2(0.5f, 0f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f),
                duration, ease, onCompleted);

        public static void ChangePivot(this RectTransform rectTransform, Vector2 newPivot)
        {
            float originalPivotX = rectTransform.pivot.x;
            float originalPivotY = rectTransform.pivot.y;
            // 在某些特定布局下，Unity会在设置Pivot时自动调整LocalPosition以试图保持UI对象不被挪动，但这个“贴心”设定充满意外惊喜需要排除
            Vector3 originalLocalPosition = rectTransform.localPosition;
            rectTransform.pivot = newPivot;
            rectTransform.localPosition = originalLocalPosition;

            rectTransform.transform.position += new Vector3((newPivot.x - originalPivotX) * rectTransform.rect.width,
                (newPivot.y - originalPivotY) * rectTransform.rect.height, 0);
        }
    }
}