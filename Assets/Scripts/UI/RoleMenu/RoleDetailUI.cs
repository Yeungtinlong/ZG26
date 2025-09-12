using System.Collections.Generic;
using TheGame.GM;
using TheGame.ResourceManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class RoleDetailUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roleNameText;
        [SerializeField] private TMP_Text _roleGradeText;
        [SerializeField] private Image _roleAvatarImage;
        [SerializeField] private List<RoleEquipSlotUI> _roleEquipSlots;

        public string RoleId { get; private set; }

        public void Set(string roleId)
        {
            RoleId = roleId;

            LCharacterConfig roleConfig = LuaToCsBridge.CharacterTable[RoleId];
            _roleNameText.text = roleConfig.Name;
            _roleGradeText.text = $"Lv.{GameRuntimeData.Instance.ChaInstances[roleConfig.Id].grade}";
            _roleAvatarImage.LoadAsyncForget(PathHelper.GetSpritePath($"Roles/ui_{RoleId}"));
        }
    }
}