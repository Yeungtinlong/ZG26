using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGame.GM;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class MissionMenuUI : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private MissionElementUI _missionElementPrefab;
        private readonly List<MissionElementUI> _missionElements = new List<MissionElementUI>();

        [SerializeField] private Button _closeButton;
        private Action<MissionMenuUI> _onClose;
        
        private void OnEnable()
        {
            SubscribeToEvents();
            RefreshUI();
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

        private void Close_OnClick()
        {
            _onClose?.Invoke(this);
        }

        private void RefreshUI()
        {
            UIHelpers.GenerateCachedListItems(
                _container,
                _missionElementPrefab,
                _missionElements,
                LuaToCsBridge.MissionTable.Values.ToList(),
                (uiElement, data) =>
                {
                    uiElement.Set(data.id,
                        data.name,
                        data.description,
                        data.icon,
                        GameRuntimeData.Instance.CompletedMissions.Contains(data.id),
                        data.showRewards,
                        Element_OnClick);
                });
        }

        private void Element_OnClick(MissionElementUI element)
        {
            MissionModel model = LuaToCsBridge.MissionTable[element.Id];
            if (GameRuntimeData.Instance.CompletedMissions.Contains(model.id))
                return;

            if (!model.canComplete.Invoke())
            {
                UIManager.Instance.OpenUI<MessagePopupUI>().Set("任务条件未达成");
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            model.showRewards.ForEach(r => stringBuilder.Append($"{LuaToCsBridge.ItemTable[r.id].Name}x{r.count},"));
            string text = stringBuilder.ToString().TrimEnd(',');
            UIManager.Instance.OpenUI<MessagePopupUI>().Set($"任务{model.name}完成，获得奖励{text}");
            GameRuntimeData.Instance.CompletedMissions.Add(model.id);
            model.onClaim.doEvent?.Invoke(null, model.onClaim.eventParams);
            GameRuntimeData.SaveGame();
            RefreshUI();
        }

        public void Set(Action<MissionMenuUI> onClose)
        {
            _onClose = onClose;
        }
    }
}