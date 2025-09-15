using System.Collections.Generic;
using TheGame.GM;
using TheGame.ResourceManagement;
using TMPro;
using UnityEngine;

namespace TheGame.UI
{
    public class RoleDetailUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roleNameText;
        [SerializeField] private TMP_Text _roleGradeText;
        [SerializeField] private TMP_Text _roleTypeText;
        [SerializeField] private TMP_Text _propertiesText;
        [SerializeField] private List<RoleEquipSlotUI> _roleEquipSlots;

        [Header("Role Animation")] [SerializeField]
        private Transform _roleStage;

        private GameObject _roleObj;

        [SerializeField] private ItemCostButtonUI _upgradeButton;

        public string RoleId { get; private set; }

        public void Set(string roleId)
        {
            RoleId = roleId;
            RefreshUI();
        }

        private void RefreshUI()
        {
            LCharacterConfig roleConfig = LuaToCsBridge.CharacterTable[RoleId];
            _roleNameText.text = roleConfig.Name;

            ChaInstance chaInstance = GameRuntimeData.Instance.ChaInstances[roleConfig.Id];
            _roleGradeText.text = $"Lv.{chaInstance.grade}";
            _roleTypeText.text = $"定位：{roleConfig.CharacterType}";

            SetEquipments();
            SetAnimation();
            SetProperties();
            SetUpgradeCost();
        }
        
        private void SetUpgradeCost()
        {
            // TODO: 偷懒，不配表了
            ChaInstance chaInstance = GameRuntimeData.Instance.ChaInstances[RoleId];
            _upgradeButton.Set(new List<ItemStack>() { new ItemStack("item_currency_coin", 100 * chaInstance.grade) },
                Upgrade_OnClick);
        }

        private void Upgrade_OnClick(ItemCostButtonUI _)
        {
            ChaInstance chaInstance = GameRuntimeData.Instance.ChaInstances[RoleId];
            List<ItemStack> cost = new List<ItemStack>()
                { new ItemStack("item_currency_coin", 100 * chaInstance.grade) };

            if (GameRuntimeData.Instance.RemoveItem(cost[0].id, cost[0].count))
            {
                chaInstance.grade++;
                GameRuntimeData.SaveGame();
                RefreshUI();
                UIManager.Instance.OpenUI<MessagePopupUI>().Set("升级成功", 1f);
            }
            else
            {
                UIManager.Instance.OpenUI<MessagePopupUI>().Set("金币不足", 1f);
            }
        }

        private void SetEquipments()
        {
            _roleEquipSlots.ForEach(s => s.Set(null));
            ChaInstance chaInstance = GameRuntimeData.Instance.ChaInstances[RoleId];
            for (int i = 0, sIdx = 0; i < chaInstance.equipments.Length; i++)
            {
                if (chaInstance.equipments[i] != null)
                {
                    _roleEquipSlots[sIdx].Set(chaInstance.equipments[i]);
                    sIdx++;
                }
            }
        }

        private void SetAnimation()
        {
            if (_roleObj != null)
                Destroy(_roleObj);

            LCharacterConfig chaConfig = LuaToCsBridge.CharacterTable[RoleId];
            _roleObj = Instantiate(
                ResLoader.LoadAsset<GameObject>(PathHelper.GetPrefabPath($"Characters/{chaConfig.Prefab}")),
                _roleStage);
        }

        private void SetProperties()
        {
            LCharacterConfig chaConfig = LuaToCsBridge.CharacterTable[RoleId];
            _propertiesText.text =
                $"生命：{chaConfig.BaseProp.hp}\n攻击：{chaConfig.BaseProp.atk}\n速度：{chaConfig.BaseProp.speed}\n";
        }
    }
}