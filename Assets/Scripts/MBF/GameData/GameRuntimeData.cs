using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using MBF;
using Newtonsoft.Json;
using UnityEngine;

namespace TheGame.GM
{
    /// <summary>
    /// NOTE: 一份运行时游戏数据，包括进度、玩家资产等，几乎所有系统都依赖于它（比如存档系统要存的就是这份数据），所以是全局的
    /// </summary>
    [Serializable]
    public class GameRuntimeData
    {
        private static GameRuntimeData _instance;

        // TODO: 这里需要做存档初始化工作
        public static GameRuntimeData Instance => _instance ??= new GameRuntimeData();

        private static Dictionary<string, List<Action<string, int>>> _listeners =
            new Dictionary<string, List<Action<string, int>>>();

        public static void AddItemCountChangeListener(string itemId, Action<string, int> listener)
        {
            _listeners.TryAdd(itemId, new List<Action<string, int>>());
            _listeners[itemId].Add(listener);
        }

        public static void RemoveItemCountChangeListener(string itemId, Action<string, int> listener)
        {
            if (!_listeners.ContainsKey(itemId)) return;
            _listeners[itemId].Remove(listener);
        }

        public static void CallOnItemCountChanged(string itemId, int count)
        {
            if (!_listeners.ContainsKey(itemId)) return;
            foreach (var listener in _listeners[itemId])
                listener?.Invoke(itemId, count);
        }

        // 运行时数据
        public int SelectedLevel { get; set; }
        public int PassedLevel { get; set; }

        public string MainActor;
        public readonly List<string> SelectedCharacters = new List<string>();

        // 拥有道具
        public Dictionary<string, int> Items = new Dictionary<string, int>();

        // 角色运行时数据
        public Dictionary<string, ChaInstance> ChaInstances = new Dictionary<string, ChaInstance>();

        // 购买记录，<商品Id, 购买次数>
        public Dictionary<string, int> PurchaseRecord = new Dictionary<string, int>();

        // 角色持有装备运行时数据
        public List<EquipInfo> EquipInfos = new List<EquipInfo>();

        public bool HasShowStory = false;

        public int CompletedOnboardingIndex = -1;

        public int SigninDays = 0;
        public DateTime LatestSigninTime;
        
        public string SelectedStrategy { get; set; }

        public List<string> CompletedMissions = new List<string>();

        public static void SaveGame()
        {
            PlayerPrefs.SetString(nameof(GameRuntimeData), JsonConvert.SerializeObject(_instance));
        }

        public static void LoadGame()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
                { ObjectCreationHandling = ObjectCreationHandling.Replace };
            _instance = JsonConvert.DeserializeObject<GameRuntimeData>(PlayerPrefs.GetString(nameof(GameRuntimeData)),
                settings);
        }

        public static bool SaveExists()
        {
            return PlayerPrefs.HasKey(nameof(GameRuntimeData));
        }

        public static void NewGame()
        {
            GameRuntimeData gameRuntimeData = new GameRuntimeData();
            foreach (var (chaId, config) in LuaToCsBridge.CharacterTable)
            {
                gameRuntimeData.ChaInstances.Add(chaId, new ChaInstance() { id = chaId, owned = false, grade = 1 });
            }

            // 默认存档
            gameRuntimeData.SelectedLevel = 1;
            gameRuntimeData.PassedLevel = 0;

            // TODO: 测试，获取所有角色
            foreach (var (chaId, config) in LuaToCsBridge.CharacterTable)
            {
                // if (config.Tags.ContainsTagNotNull("playerActor"))
                //     gameRuntimeData.GetCharacter(chaId);
                // continue;
                if (config.Tags.ContainsTagNotNull("default"))
                {
                    gameRuntimeData.GetCharacter(chaId);
                }
            }

            foreach (var defaultEquip in LuaToCsBridge.DefaultEquipTable.Values)
            {
                foreach (var eid in defaultEquip.equipIds)
                {
                    gameRuntimeData.AddEquipToCha(eid, defaultEquip.chaId);
                }
            }

            // foreach (var (eId, eModel) in LuaToCsBridge.EquipmentTable)
            //     instance.GetItem(eId, 1);
            gameRuntimeData.GetItem("item_currency_coin", 500);
            gameRuntimeData.SelectedStrategy = "strategy_common";
            _instance = gameRuntimeData;
        }

        public void GetCharacter(string chaId)
        {
            ChaInstances[chaId].owned = true;
        }

        public void GetItem(string itemId, int count)
        {
            if (count <= 0) return;

            if (!Items.ContainsKey(itemId))
                Items[itemId] = 0;
            Items[itemId] += count;
            CallOnItemCountChanged(itemId, Items[itemId]);
        }

        public bool RemoveItem(string itemId, int count)
        {
            if (count <= 0 || !Items.ContainsKey(itemId) || Items[itemId] < count)
                return false;
            Items[itemId] -= count;
            CallOnItemCountChanged(itemId, Items[itemId]);
            return true;
        }

        public int CountOfItem(string itemId) => Items.GetValueOrDefault(itemId, 0);

        public void AddEquipToCha(string equipId, string chaId)
        {
            EquipmentModel equipmentModel = LuaToCsBridge.EquipmentTable[equipId];
            ChaInstance chaInstance = ChaInstances[chaId];
            if (chaInstance.equipments[(int)equipmentModel.slot] != null)
            {
                RemoveEquipFromCha(chaInstance.equipments[(int)equipmentModel.slot], chaId);
            }

            EquipInfos.Add(new EquipInfo() { itemId = equipId, chaId = chaId });
            chaInstance.equipments[(int)equipmentModel.slot] = equipId;
        }

        public void RemoveEquipFromCha(string equipId, string chaId)
        {
            ChaInstance chaInstance = ChaInstances[chaId];
            EquipmentModel equipmentModel = LuaToCsBridge.EquipmentTable[equipId];

            int idx = EquipInfos.FindIndex(e => e.chaId == chaId && e.itemId == equipId);
            if (idx >= 0)
            {
                EquipInfos.RemoveAt(idx);
                chaInstance.equipments[(int)equipmentModel.slot] = null;
            }
        }

        public int GetFreeEquipCount(string itemId)
        {
            int total = CountOfItem(itemId);
            int equipped = EquipInfos.Count(e => e.itemId == itemId);
            return total - equipped;
        }

        public bool CheckProductValid(string productId)
        {
            LProductConfig productConfig = LuaToCsBridge.ShopTable[productId];
            return this.PurchaseRecord.ContainsKey(productId) && productConfig.Limits <= this.PurchaseRecord[productId];
        }

        public bool Purchase(string productId)
        {
            LProductConfig productConfig = LuaToCsBridge.ShopTable[productId];
            if (!RemoveItem(productConfig.Price.id, productConfig.Price.count))
                return false;

            if (!PurchaseRecord.ContainsKey(productId))
                PurchaseRecord.Add(productId, 0);

            PurchaseRecord[productId]++;
            return (bool)productConfig.Effect.doEvent.Invoke(null, productConfig.Effect.eventParams);
        }
    }
}