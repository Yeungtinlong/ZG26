using System;
using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public interface IFadeCanvasGroupDOTweenAnimatable : IDOTweenAnimatable
    {
        public float FADE_DURATION { get; }
        public float FADE_ALPHA { get; }

        public CanvasGroup CanvasGroup { get; }

        void IDOTweenAnimatable.Show(Action onComplete)
        {
            SetClose();
            CanvasGroup.interactable = false;
            CanvasGroup.DOFade(FADE_ALPHA, FADE_DURATION)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    CanvasGroup.interactable = true;
                    onComplete?.Invoke();
                });
        }

        void IDOTweenAnimatable.Hide(Action onComplete)
        {
            SetOpen();
            CanvasGroup.interactable = false;
            CanvasGroup.DOFade(0f, FADE_DURATION)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        void IDOTweenAnimatable.SetOpen()
        {
            CanvasGroup.alpha = FADE_ALPHA;
        }

        void IDOTweenAnimatable.SetClose()
        {
            CanvasGroup.alpha = 0f;
        }
    }
}