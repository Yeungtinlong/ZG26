using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SupportUtils
{
    public interface IFadeDOTweenAnimatable : IDOTweenAnimatable
    {
        private const float FADE_DURATION = 0.25f;
        public float FADE_ALPHA { get; }

        public Image Image { get; }

        void IDOTweenAnimatable.Show(Action onComplete)
        {
            SetClose();
            Image.DOFade(FADE_ALPHA, FADE_DURATION)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        void IDOTweenAnimatable.Hide(Action onComplete)
        {
            SetOpen();
            Image.DOFade(0f, FADE_DURATION)
                .SetEase(Ease.InQuad)
                .SetUpdate(true)
                .OnComplete(() => onComplete?.Invoke());
        }

        void IDOTweenAnimatable.SetOpen()
        {
            Color color = Image.color;
            color.a = FADE_ALPHA;
            Image.color = color;
        }

        void IDOTweenAnimatable.SetClose()
        {
            Color color = Image.color;
            color.a = 0f;
            Image.color = color;
        }
    }
}