using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using TheGame.ResourceManagement;

namespace TheGame.UI
{
    public class MissionElementUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _claimButton;
        [SerializeField] private GameObject _completedObject;
        [SerializeField] private ItemStacksInspectorUI _rewardsInspector;

        private Action<MissionElementUI> _onClick;

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
            _claimButton.onClick.AddListener(ClaimButton_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _claimButton.onClick.RemoveListener(ClaimButton_OnClick);
        }

        private void ClaimButton_OnClick()
        {
            _onClick?.Invoke(this);
        }

        public void Set(string id, string name, string description, string icon, bool completed, List<ItemStack> rewards, Action<MissionElementUI> onClick)
        {
            Id = id;
            _nameText.text = name;
            _descriptionText.text = description;
            _iconImage.LoadAsyncForget(PathHelper.GetSpritePath(icon));
            _completedObject.SetActive(completed);
            _rewardsInspector.Set(rewards);
            _onClick = onClick;
        }
    }
}