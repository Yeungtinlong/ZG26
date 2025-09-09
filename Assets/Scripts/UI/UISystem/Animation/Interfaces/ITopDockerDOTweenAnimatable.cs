using System;
using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public interface ITopDockerDOTweenAnimatable : IDOTweenAnimatable
    {
        public RectTransform Content { get; }

        void IDOTweenAnimatable.Show(Action onComplete)
        {
            SetClose();
            Content
                .DOLocalMoveY((Content.pivot.y - 1f) * Content.rect.height, 0.5f)
                .SetUpdate(true)
                .SetEase(Ease.OutBack)
                .OnComplete(() => onComplete?.Invoke());
        }

        void IDOTweenAnimatable.SetOpen()
        {
            Content.DOKill();
            var localPos = Content.localPosition;
            localPos.y = (Content.pivot.y - 1f) * Content.rect.height;
            Content.localPosition = localPos;
        }

        void IDOTweenAnimatable.Hide(Action onComplete)
        {
            // SetOpen();
            Content
                .DOLocalMoveY(Content.rect.height, 0.5f)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        void IDOTweenAnimatable.SetClose()
        {
            Content.DOKill();
            var localPos = Content.localPosition;
            localPos.y = Content.pivot.y * Content.rect.height;
            Content.localPosition = localPos;
        }
    }
}