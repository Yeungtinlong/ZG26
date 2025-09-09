using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public class BottomDockerDOTweenAnimatable : MonoBehaviour, IBottomDockerDOTweenAnimatable
    {
        [field: SerializeField] public RectTransform Content { get; private set; }

        private void OnDisable()
        {
            Content.DOKill();
        }
    }
}