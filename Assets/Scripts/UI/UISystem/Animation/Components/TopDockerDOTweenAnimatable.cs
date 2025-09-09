using DG.Tweening;
using UnityEngine;

namespace SupportUtils
{
    public class TopDockerDOTweenAnimatable : MonoBehaviour, ITopDockerDOTweenAnimatable
    {
        [field: SerializeField] public RectTransform Content { get; private set; }
        
        private void OnDisable()
        {
            Content.DOKill();
        }
    }
}