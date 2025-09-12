using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class DailyElementUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Button _btn;
        [SerializeField] private GameObject _completeObj;
        
        public int Day { get; private set; }
        private Action<DailyElementUI> _onClick;

        private void OnEnable()
        {
            _btn.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke(this);
        }

        public void Set(int day, string title, string description, Sprite icon, bool isCompleted, Action<DailyElementUI> onClick)
        {
            Day = day;
            _onClick = onClick;
            _titleText.text = title;
            _descriptionText.text = description;
            _iconImage.sprite = icon;
            _completeObj.SetActive(isCompleted);
        }
    }
}