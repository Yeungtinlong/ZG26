using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public class PopupShowOnlyDOTweenAnimatable : MonoBehaviour, IPopupShowOnlyDOTweenAnimatable
    {
        [field: SerializeField] public RectTransform Content { get; private set; }

        private void OnDisable()
        {
            Content.DOKill();
        }
    }
}