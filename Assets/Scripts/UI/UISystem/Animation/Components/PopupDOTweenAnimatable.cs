using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public class PopupDOTweenAnimatable : MonoBehaviour, IPopupDOTweenAnimatable
    {
        [field: SerializeField] public RectTransform Content { get; private set; }

        private void OnDisable()
        {
            Content.DOKill();
        }
    }
}