using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public class FadeCanvasGroupDOTweenAnimatable : MonoBehaviour, IFadeCanvasGroupDOTweenAnimatable
    {
        [field: SerializeField] public float FADE_DURATION { get; private set; } = 0.25f;
        [field: SerializeField] public float FADE_ALPHA { get; private set; } = 1f;
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

        private void OnDisable()
        {
            CanvasGroup.DOKill();
        }
    }
}