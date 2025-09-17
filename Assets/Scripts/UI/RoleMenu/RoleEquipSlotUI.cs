using System;
using MBF;
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
        [SerializeField] private TMP_Text _equipSlotText;
        [SerializeField] private GameObject _equipSlotObject;
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

        public void Set(string equipId, Action<RoleEquipSlotUI> onClick = null)
        {
            EquipId = equipId;
            _onClick = onClick;
            if (string.IsNullOrEmpty(EquipId))
            {
                _equipAvatarImage.sprite = null;
                _equipAvatarImage.gameObject.SetActive(false);
                _equipSlotObject.SetActive(false);
                _equipNameText.text = null;
                return;
            }

            EquipmentModel model = LuaToCsBridge.EquipmentTable[EquipId];
            _equipNameText.text = model.name;
            _equipSlotText.text = model.slot switch
            {
                EquipmentSlot.Weapon => "武器",
                EquipmentSlot.Helmet => "头盔",
                EquipmentSlot.Armor => "上衣",
                EquipmentSlot.Shoe => "鞋",
                EquipmentSlot.Relic => "法宝",
                EquipmentSlot.Horse => "坐骑",
                _ => throw new ArgumentOutOfRangeException()
            };
            _equipAvatarImage.gameObject.SetActive(true);
            _equipSlotObject.SetActive(true);
            _equipAvatarImage.LoadAsyncForget(PathHelper.GetSpritePath($"Items/ui_head_{equipId}"));
        }
    }
}