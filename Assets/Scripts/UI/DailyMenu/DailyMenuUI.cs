using System;
using System.Collections.Generic;
using TheGame.GM;
using TheGame.ResourceManagement;
using UnityEngine;

namespace TheGame.UI
{
    public class DailyMenuUI : MonoBehaviour, INavigationMenu
    {
        [SerializeField] private List<DailyElementUI> _dailyElements;

        public NavigationMenuType Type => NavigationMenuType.Daily;

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
            RefreshUI();
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
                    dailyModel.description,
                    ResLoader.LoadAsset<Sprite>(PathHelper.GetSpritePath(dailyModel.icon)),
                    (GameRuntimeData.Instance.SigninDays % 7) >= day,
                    Daily_OnClick
                );
            }
        }

        private void Daily_OnClick(DailyElementUI element)
        {
            if ((GameRuntimeData.Instance.SigninDays % 7) + 1 != element.Day) return;
            if (DateTime.Now.Date == GameRuntimeData.Instance.LatestSigninTime.Date) return;

            GameRuntimeData.Instance.LatestSigninTime = DateTime.Now;
            GameRuntimeData.Instance.SigninDays++;

            DailyModel dailyModel = LuaToCsBridge.DailyTable[element.Day];
            dailyModel.effect.doEvent?.Invoke(null, dailyModel.effect.eventParams);
            UIManager.Instance.OpenUI<MessagePopupUI>().Set($"签到成功，获得{dailyModel.description}", 1f);

            GameRuntimeData.SaveGame();

            RefreshUI();
        }
    }
}