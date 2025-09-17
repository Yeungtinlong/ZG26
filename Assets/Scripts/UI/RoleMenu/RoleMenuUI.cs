using System;
using System.Collections.Generic;
using System.Linq;
using TheGame.GM;
using UnityEngine;

namespace TheGame.UI
{
    public class RoleMenuUI : MonoBehaviour, INavigationMenu
    {
        [Header("Role Name Scrollbar")]
        [SerializeField]
        private Transform _roleElesContainer;

        [SerializeField] private RoleElementUI _roleElementPrefab;
        private readonly List<RoleElementUI> _roleElements = new List<RoleElementUI>();

        [SerializeField] private RoleDetailUI _roleDetail;

        private string _selectedRoleId;

        public NavigationMenuType Type => NavigationMenuType.Role;

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
        }

        private void UnsubscribeFromEvents()
        {
        }

        public void Set()
        {
            _selectedRoleId = LuaToCsBridge.CharacterTable.Values.First().Id;
            RefreshUI();
        }

        private void RefreshUI()
        {
            UIHelpers.GenerateCachedListItems(
                _roleElesContainer,
                _roleElementPrefab,
                _roleElements,
                LuaToCsBridge.CharacterTable.Values
                    .Where(role => role.Tags.Contains("playerActor") && GameRuntimeData.Instance.ChaInstances[role.Id].owned)
                    .Select(role => role.Id)
                    .ToList(),
                (ele, roleId) => ele.Set(roleId, _selectedRoleId == roleId, RoleElementUI_OnClick)
            );
            _roleDetail.Set(_selectedRoleId);
        }

        private void RoleElementUI_OnClick(RoleElementUI roleElementUI)
        {
            _selectedRoleId = roleElementUI.RoleId;
            RefreshUI();
        }
    }
}