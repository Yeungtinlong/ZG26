using System.Collections.Generic;
using System.Linq;
using MBF;
using TheGame.Common;
using XLua;

namespace TheGame.GM
{
    public static class LuaToCsBridge
    {
        private static LuaEnv _luaEnv;

        public static Dictionary<string, LCharacterConfig> CharacterTable;
        public static Dictionary<int, LevelModel> LevelTable;
        public static Dictionary<string, SkillModel> SkillTable;
        public static Dictionary<string, EquipmentModel> EquipmentTable;
        public static Dictionary<string, LProductConfig> ShopTable;
        public static Dictionary<string, LSummonConfig> SummonTable;
        public static Dictionary<string, LItemConfig> ItemTable;
        public static Dictionary<int, DailyModel> DailyTable;
        public static Dictionary<string, RoleDefaultEquipModel> DefaultEquipTable;
        public static string StoryText;

        private static void Print(string msg)
        {
            return;
            // Debug.Log($"{msg}");
        }

        public static void LoadLuaConfigs()
        {
            _luaEnv = LuaManager.LuaEnv;
            _luaEnv.DoString("Game.InitLuaTables();");

            StoryText = _luaEnv.Global.GetInPath<string>("Game.Designer.Story");
            Print($"[LuaConfigToCsInit] StoryText {StoryText.Length} success");

            CharacterTable = _luaEnv.Global.GetInPath<List<LCharacterConfig>>("Game.Designer.Character")
                .ToDictionary(k => k.Id, v => v);
            Print($"[LuaConfigToCsInit] CharacterTable {CharacterTable.Count} success");

            List<LevelModel> levels = _luaEnv.Global.GetInPath<List<LevelModel>>("Game.Designer.Level");
            LevelTable = levels.ToDictionary(k => k.id, v => v);
            Print($"[LuaConfigToCsInit] LevelTable {LevelTable.Count} success");

            ShopTable = _luaEnv.Global.GetInPath<List<LProductConfig>>("Game.Designer.Shop")
                .ToDictionary(k => k.Id, v => v);
            Print($"[LuaConfigToCsInit] ShopTable {ShopTable.Count} success");

            ItemTable = _luaEnv.Global.GetInPath<List<LItemConfig>>("Game.Designer.Item")
                .ToDictionary(k => k.Id, v => v);
            Print($"[LuaConfigToCsInit] ItemTable {ItemTable.Count} success");

            SummonTable = _luaEnv.Global.GetInPath<List<LSummonConfig>>("Game.Designer.Summon")
                .ToDictionary(k => k.Id, v => v);
            Print($"[LuaConfigToCsInit] SummonTable {SummonTable.Count} success");

            EquipmentTable = _luaEnv.Global.GetInPath<Dictionary<string, EquipmentModel>>("Game.Designer.Equipment");
            Print($"[LuaConfigToCsInit] EquipmentTable {EquipmentTable.Count} success");

            SkillTable = _luaEnv.Global.GetInPath<Dictionary<string, SkillModel>>("Game.Designer.Skill");
            Print($"[LuaConfigToCsInit] SkillTable {SkillTable.Count} success");

            DailyTable = _luaEnv.Global.GetInPath<List<DailyModel>>("Game.Designer.Daily")
                .ToDictionary(k => k.day, v => v);
            Print($"[LuaConfigToCsInit] DailyTable {DailyTable.Count} success");

            DefaultEquipTable = _luaEnv.Global.GetInPath<List<RoleDefaultEquipModel>>("Game.Designer.DefaultEquip")
                .ToDictionary(k => k.chaId, v => v);
            Print($"[LuaConfigToCsInit] DefaultEquipTable {DefaultEquipTable.Count} success");

            DesignerFormula.Init();
        }
    }
}