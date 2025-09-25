using System;
using TheGame.ResourceManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class StrategyElementUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _selectButton;
        [SerializeField] private GameObject _selectedObject;
        
        [SerializeField] private GameObject _lockedObject;
        [SerializeField] private TMP_Text _lockedText;

        private Action<StrategyElementUI> _onClick;

        public string Id { get; private set; }

        void OnEnable()
        {
            SubscribeToEvents();
        }

        void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _selectButton.onClick.AddListener(Select_OnClick);
        }


        private void UnsubscribeFromEvents()
        {
            _selectButton.onClick.RemoveListener(Select_OnClick);
        }

        private void Select_OnClick()
        {
            _onClick?.Invoke(this);
        }

        public void Set(string id, string strategyName, string description, bool selected, bool locked, string lockedText, Action<StrategyElementUI> onClick)
        {
            Id = id;
            _nameText.text = strategyName;
            _descriptionText.text = description;
            _lockedText.text = lockedText;
            _lockedObject.SetActive(locked);
            _selectedObject.SetActive(selected);
            _iconImage.LoadAsyncForget(PathHelper.GetSpritePath($"Icons/ui_icon_{id}"));
            _onClick = onClick;
        }
    }
}