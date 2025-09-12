using System;
using TheGame.GM;
using TheGame.ResourceManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class RoleEquipSlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _equipNameText;
        [SerializeField] private Image _equipAvatarImage;
        [SerializeField] private Button _button;
        public string EquipId { get; private set; }

        private Action<RoleEquipSlotUI> _onClick;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _onClick?.Invoke(this);
        }

        public void Set(string equipId, Action<RoleEquipSlotUI> onClick)
        {
            EquipId = equipId;
            _onClick = onClick;
            _equipNameText.text = LuaToCsBridge.EquipmentTable[EquipId].name;
            _equipAvatarImage.LoadAsyncForget(PathHelper.GetSpritePath($"ui_head_{equipId}"));
        }
    }
}