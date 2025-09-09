using System;
using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public interface IPopupDOTweenAnimatable : IDOTweenAnimatable
    {
        private const float SCALE_DURATION = 0.25f;
        private static readonly Vector2 START_SCALE = 0.50f * Vector2.one;
        
        public RectTransform Content { get; }

        void IDOTweenAnimatable.Show(Action onComplete)
        {
            SetClose();
            Content.DOScale(Vector3.one, SCALE_DURATION)
                .SetEase(Ease.OutBack)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        void IDOTweenAnimatable.Hide(Action onComplete)
        {
            SetOpen();
            Content.DOScale(START_SCALE, SCALE_DURATION)
                .SetEase(Ease.InBack)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        void IDOTweenAnimatable.SetOpen()
        {
            Content.DOKill();
            Content.localScale = Vector3.one;
        }

        void IDOTweenAnimatable.SetClose()
        {
            Content.DOKill();
            Content.localScale = START_SCALE;
        }
    }
}