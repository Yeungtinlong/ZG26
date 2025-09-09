using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TheGame.ResourceManagement;

namespace TheGame.UI
{
    public class ItemContainerUI : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Button _clickable;

        private string _spritePath;

        public event Action<ItemContainerUI> OnClick;

        private void OnEnable()
        {
            _clickable.onClick.AddListener(Clickable_OnClick);
        }

        private void OnDisable()
        {
            _clickable.onClick.RemoveListener(Clickable_OnClick);
        }

        private void Clickable_OnClick()
        {
            OnClick?.Invoke(this);
        }

        public void Set(string spritePath, int count)
        {
            _spritePath = spritePath;
            _iconImage.LoadAsyncForget(_spritePath);
            _amountText.text = $"{count}";
        }

        public void Set(int count)
        {
            _amountText.text = $"{count}";
        }
    }
}