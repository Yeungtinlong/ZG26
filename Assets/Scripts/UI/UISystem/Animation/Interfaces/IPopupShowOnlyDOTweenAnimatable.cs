using System;
using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public interface IPopupShowOnlyDOTweenAnimatable : IDOTweenAnimatable
    {
        private const float SCALE_DURATION = 0.25f;
        private const float START_SCALE = 0.5f;

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
            onComplete?.Invoke();
        }

        void IDOTweenAnimatable.SetOpen()
        {
            Content.DOKill();
            Content.localScale = Vector3.one;
        }

        void IDOTweenAnimatable.SetClose()
        {
            Content.DOKill();
            Content.localScale = Vector3.one * START_SCALE;
        }
    }
}