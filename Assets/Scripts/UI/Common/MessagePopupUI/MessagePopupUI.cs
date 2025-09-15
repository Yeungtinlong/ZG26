using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TheGame.UI
{
    public class MessagePopupUI : BaseUI, IOneShotUI
    {
        public override UILayer Layer => UILayer.Popup;
        [SerializeField] private TMP_Text _messageText;
        private CanvasGroup _canvasGroup;

        private Sequence _sequence;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }

        public void Set(string message, float duration)
        {
            _messageText.text = message;

            _sequence?.Kill();
            _canvasGroup.alpha = 0;

            _sequence = DOTween.Sequence();
            _sequence.Append(_canvasGroup.DOFade(1f, 0.2f));
            _sequence.AppendInterval(duration);
            _sequence.Append(_canvasGroup.DOFade(0f, 0.2f));

            _sequence.OnComplete(() => uiManager.CloseUI(this));
        }
    }
}