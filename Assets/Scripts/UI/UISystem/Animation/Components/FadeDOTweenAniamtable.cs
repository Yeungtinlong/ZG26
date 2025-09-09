using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SupportUtils
{
    public class FadeDoTweenAnimatable : MonoBehaviour, IFadeDOTweenAnimatable
    {
        [field: SerializeField] public Image Image { get; private set; }
        [field: SerializeField] public float FADE_ALPHA { get; private set; } = 0.96f;

        private void OnDisable()
        {
            Image.DOKill();
        }
    }
}