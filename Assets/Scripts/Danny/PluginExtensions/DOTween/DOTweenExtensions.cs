// #define DANNY_DOTWEEN_SUPPORT
#if DANNY_DOTWEEN_SUPPORT
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;
using Ease = DG.Tweening.Ease;

namespace SupportUtils
{
    public static class DOTweenExtensions
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
            Color originColor = menuBg.color;
            Color endColor = originColor;
            endColor.a = alphaTo;
            menuBg.color = new Color(originColor.r, originColor.g, originColor.b, alphaFrom);

            // DG.Tweening.ShortcutExtensions.DOFade();

            return DOTween.To(() => menuBg.color, value => menuBg.color = value, endColor, duration)
                .OnComplete(() => onCompleted?.Invoke());

            // return menuBg.DOFade(alphaTo, duration).OnComplete(() => onCompleted?.Invoke());
        }

        public static TweenerCore<Color, Color, ColorOptions> MenuBgFadeIn(this Image menuBg,
            float alphaTo = 1f,
            float duration = 1f,
            Action onCompleted = null)
            => MenuBgFade(menuBg, 0f, alphaTo, duration, onCompleted);

        public static TweenerCore<Color, Color, ColorOptions> MenuBgFadeOut(this Image menuBg,
            float alphaFrom = 1f,
            float duration = 1f,
            Action onCompleted = null)
            => MenuBgFade(menuBg, alphaFrom, 0f, duration, onCompleted);


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


        public static TweenerCore<Vector3, Vector3, VectorOptions> PopTip(this Transform popupContent,
            float startScale, float targetScale, float duration, Action onCompleted = null)
        {
            popupContent.DOKill();
            popupContent.localScale = startScale * Vector3.one;
            return popupContent.DOScale(targetScale, duration * 0.5f).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => onCompleted?.Invoke());
        }


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

        // WIP: 未实现菜单切入、切出。
        public static void ContentMove(this RectTransform rectTransform, float duration, Action onCompleted = null) { }

        public static void LogoScaleFade(this Image logo, float alphaFrom, float alphaTo, Vector3 scaleFrom,
            Vector3 scaleTo, float duration, bool isResetAfterCompleted = true, Action onCompleted = null)
        {
            Transform logoTransform = logo.transform;

            logo.DOKill();
            logoTransform.DOKill();

            Color originColor = logo.color;
            Color startColor = originColor;
            startColor.a = alphaFrom;
            logo.color = startColor;
            Color endColor = startColor;
            endColor.a = alphaTo;

            Vector3 originScale = logoTransform.localScale;
            logoTransform.localScale = scaleFrom;

            Sequence seq = DOTween.Sequence()
                // logo.DOFade(alphaTo, duration)
                .Join(DOTween.To(() => logo.color, value => logo.color = value, endColor, duration))
                .Join(logoTransform.DOScale(scaleTo, duration))
                .OnComplete(() =>
                {
                    if (isResetAfterCompleted)
                    {
                        logo.color = originColor;
                        logoTransform.localScale = originScale;
                    }

                    onCompleted?.Invoke();
                });
        }

        public static void LogoScaleFadeOut(this Image logo, float duration, bool isResetAfterCompleted = true,
            Action onCompleted = null)
            => LogoScaleFade(logo, logo.color.a, 0f, Vector3.one, Vector3.one * 2f, duration, isResetAfterCompleted,
                onCompleted);

        public static void LogoScaleFadeIn(this Image logo, float duration, bool isResetAfterCompleted = true,
            Action onCompleted = null)
            => LogoScaleFade(logo, 0f, logo.color.a, Vector3.one * 2f, Vector3.one, duration, isResetAfterCompleted,
                onCompleted);
    }
}
#endif