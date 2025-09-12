using System;
using TheGame.GM;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class RoleElementUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roleNameText;
        [SerializeField] private Image _selectedImage;
        [SerializeField] private Sprite _selectedSprite;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Button _button;

        private Action<RoleElementUI> _onClick;

        public string RoleId { get; private set; }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke(this);
        }

        public void Set(string roleId, bool selected, Action<RoleElementUI> onClick)
        {
            RoleId = roleId;
            _onClick = onClick;
            _selectedImage.sprite = selected ? _selectedSprite : _normalSprite;
            _roleNameText.text = LuaToCsBridge.CharacterTable[RoleId].Name;
        }
    }
}