using System;
using System.Collections.Generic;
using System.Linq;
using TheGame.GM;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class RoleMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        [Header("Role Name Scrollbar")] [SerializeField]
        private Transform _roleElesContainer;

        [SerializeField] private RoleElementUI _roleElementPrefab;
        private readonly List<RoleElementUI> _roleElements = new List<RoleElementUI>();

        [SerializeField] private RoleDetailUI _roleDetail;

        private string _selectedRoleId;
        private Action _onClose;

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
            _closeButton.onClick.AddListener(Close_OnClick);
        }

        private void UnsubscribeFromEvents()
        {
            _closeButton.onClick.RemoveListener(Close_OnClick);
        }

        public void Set(Action onClose)
        {
            _onClose = onClose;
            _selectedRoleId = LuaToCsBridge.CharacterTable.Values.First().Id;
            RefreshUI();
        }

        private void Close_OnClick()
        {
            _onClose?.Invoke();
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