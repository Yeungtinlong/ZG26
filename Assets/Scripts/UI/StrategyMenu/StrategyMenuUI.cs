using System.Collections.Generic;
using System.Linq;
using TheGame.GM;
using UnityEngine;

namespace TheGame.UI
{
    public class StrategyMenuUI : MonoBehaviour, INavigationMenu
    {
        [SerializeField] private Transform _container;
        [SerializeField] private StrategyElementUI _strategyElementPrefab;
        private readonly List<StrategyElementUI> _strategyElements = new List<StrategyElementUI>();

        public NavigationMenuType Type => NavigationMenuType.Strategy;

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
        }

        private void UnsubscribeFromEvents()
        {
        }

        private void RefreshUI()
        {
            UIHelpers.GenerateCachedListItems(
                _container,
                _strategyElementPrefab,
                _strategyElements,
                LuaToCsBridge.StrategyTable.Values.ToList(),
                (uiElement, data) =>
                {
                    uiElement.Set(data.id, data.name, data.description,
                        GameRuntimeData.Instance.SelectedStrategy == data.id,
                        !CheckUnlocked(data.id, out StrategyModel strategyModel), strategyModel.unlockDescription,
                        SelectStrategy_OnClick);
                });
        }

        private bool CheckUnlocked(string id, out StrategyModel strategyModel)
        {
            strategyModel = LuaToCsBridge.StrategyTable[id];
            if (strategyModel.unlockCondition != null && strategyModel.unlockCondition.Invoke())
                return true;
            return false;
        }

        private void SelectStrategy_OnClick(StrategyElementUI element)
        {
            if (CheckUnlocked(element.Id, out StrategyModel strategyModel))
            {
                GameRuntimeData.Instance.SelectedStrategy = element.Id;
                GameRuntimeData.SaveGame();
                RefreshUI();
            }
        }

        public void Set()
        {
        }
    }
}