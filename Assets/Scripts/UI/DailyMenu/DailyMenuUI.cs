using System;
using System.Collections.Generic;
using TheGame.GM;
using TheGame.ResourceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace TheGame.UI
{
    public class DailyMenuUI : MonoBehaviour
    {
        [SerializeField] private List<DailyElementUI> _dailyElements;
        [SerializeField] private Button _closeButton;
        private Action _onClose;

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

        public void Set(Action onClose)
        {
            _onClose = onClose;
        }
        
        private void Close_OnClick()
        {
            _onClose?.Invoke();
        }

        private void RefreshUI()
        {
            for (int i = 0; i < _dailyElements.Count; i++)
            {
                int day = i + 1;
                DailyModel dailyModel = LuaToCsBridge.DailyTable[day];
                _dailyElements[i].Set(
                    day,
                    $"第{day}日",
                    $"{LuaToCsBridge.ItemTable[dailyModel.rewards[0].id].Name}x{dailyModel.rewards[0].count}",
                    ResLoader.LoadAsset<Sprite>(PathHelper.GetSpritePath($"Items/ui_head_{dailyModel.rewards[0].id}")),
                    (GameRuntimeData.Instance.SigninDays % 7) >= day,
                    Daily_OnClick
                );
            }
        }

        private void Daily_OnClick(DailyElementUI element)
        {
            if ((GameRuntimeData.Instance.SigninDays % 7) + 1 != element.Day) return;
            if ((DateTime.Now - GameRuntimeData.Instance.LatestSigninTime).TotalDays < 1) return;
            
            GameRuntimeData.Instance.LatestSigninTime = DateTime.Now;
            GameRuntimeData.Instance.SigninDays++;

            DailyModel dailyModel = LuaToCsBridge.DailyTable[element.Day];
            foreach (var itemStack in dailyModel.rewards)
                GameRuntimeData.Instance.GetItem(itemStack.id, itemStack.count);
            
            UIManager.Instance.OpenUI<MessagePopupUI>().Set($"签到成功，获得{LuaToCsBridge.ItemTable[dailyModel.rewards[0].id].Name}x{dailyModel.rewards[0].count}", 1f);
            
            GameRuntimeData.SaveGame();
            
            RefreshUI();
        }
    }
}