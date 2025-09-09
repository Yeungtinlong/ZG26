using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TheGame.UI
{
    public class PopMessageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;

        private Sequence _animationSequence;
        public float duration { get; set; }

        private void OnDisable()
        {
            _animationSequence?.Kill();
        }

        public void Set(string message, float scale, Color color)
        {
            _damageText.text = message;

            _animationSequence?.Kill();
            _animationSequence = DOTween.Sequence();

            _damageText.transform.localPosition = Vector3.zero;
            _damageText.transform.localScale = Vector3.one * 0.1f;
            _damageText.alpha = 1f;
            _damageText.color = color;

            float targetScale = 1f;
            duration = 1f;

            _animationSequence
                .Append(_damageText.transform.DOScale(targetScale, 0.5f)
                    .SetEase(Ease.OutBack))
                .Append(_damageText.transform.DOLocalMoveY(1f, 0.25f))
                .Join(_damageText.DOFade(0f, 0.25f));
        }
    }
}