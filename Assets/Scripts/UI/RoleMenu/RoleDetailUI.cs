using System;
using System.Collections.Generic;
using MBF;
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
        [SerializeField] private TMP_Text _roleTypeText;
        [SerializeField] private TMP_Text _rarityText;

        [SerializeField] private TMP_Text _propertiesText;
        [SerializeField] private List<RoleEquipSlotUI> _roleEquipSlots;

        [Header("Role Animation")] [SerializeField]
        private Transform _roleStage;

        private GameObject _roleObj;

        [SerializeField] private ItemCostButtonUI _upgradeButton;

        public string RoleId { get; private set; }

        private static readonly Dictionary<Rarity, Color> _rarityColors = new Dictionary<Rarity, Color>
        {
            { Rarity.Normal, new Color(.2f, 1f, .6f) },
            { Rarity.Rare, new Color(0, .5f, 1f) },
            { Rarity.SuperRare, new Color(.5f, .0f, 1f) },
            { Rarity.Legendary, new Color(1f, .0f, .5f) },
            { Rarity.Mythic, new Color(1f, .6f, .3f) },
        };
        
        private static readonly Dictionary<CharacterType, string> _typeTexts = new Dictionary<CharacterType, string>
        {
            { CharacterType.Tank, "坦克" },
            { CharacterType.Warrior, "战士" },
            { CharacterType.Carry, "法师" },
            { CharacterType.Support, "特殊" },
            { CharacterType.Assassin, "刺客" },
        };

        public void Set(string roleId)
        {
            RoleId = roleId;
            RefreshUI();
        }

        private void RefreshUI()
        {
            LCharacterConfig roleConfig = LuaToCsBridge.CharacterTable[RoleId];
            _roleNameText.text = roleConfig.Name;
            _roleNameText.color = _rarityColors[roleConfig.Rarity];
            ChaInstance chaInstance = GameRuntimeData.Instance.ChaInstances[roleConfig.Id];
            _roleGradeText.text = $"Lv.{chaInstance.grade}";
            _roleTypeText.text = $"定位：{_typeTexts[roleConfig.CharacterType]}";
            _rarityText.text =
                $"稀有度: {roleConfig.Rarity switch { Rarity.Normal => "N", Rarity.Rare => "R", Rarity.SuperRare => "SR", Rarity.Legendary => "SSR", Rarity.Mythic => "SSS", _ => throw new ArgumentOutOfRangeException() }}";
            _rarityText.color = _rarityColors[roleConfig.Rarity];
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
            ChaInstance chaInstance = GameRuntimeData.Instance.ChaInstances[RoleId];
            ChaProp baseProp = DesignerFormula.GetGradeProp(
                chaConfig.BaseProp,
                chaConfig.PropGrowth[0],
                chaConfig.PropGrowth[1],
                chaInstance.grade
            );
            ChaProp[] equipProps = new ChaProp[2] { ChaProp.zero, ChaProp.zero };

            for (int i = 0; i < chaInstance.equipments.Length; i++)
            {
                if (string.IsNullOrEmpty(chaInstance.equipments[i]))
                    continue;

                EquipmentModel equipmentModel = LuaToCsBridge.EquipmentTable[chaInstance.equipments[i]];
                for (int j = 0; j < Mathf.Min(2, equipmentModel.propMod.Length); j++)
                    equipProps[j] += equipmentModel.propMod[j];
            }

            ChaProp finalProps = (baseProp + equipProps[0]) * equipProps[1];

            _propertiesText.text =
                $"生命：{finalProps.hp}\n攻击：{finalProps.atk}\n速度：{finalProps.speed}\n";
        }
    }
}