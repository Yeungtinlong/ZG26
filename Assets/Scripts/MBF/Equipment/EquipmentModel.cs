using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public enum EquipmentSlot
    {
        Weapon = 0,
        Helmet = 1,
        Armor = 2,
        Shoe = 3,
        Relic = 4,
    }

    [XLua.LuaCallCSharp]
    public struct EquipmentModel
    {
        public string id;
        public string name;
        public string[] tags;
        public string description;
        public EquipmentSlot slot;
        public ChaProp[] propMod;
        public AddBuffInfo[] addBuffs;

        public EquipmentModel(string id, string name, string[] tags, string description, EquipmentSlot slot, ChaProp[] propMod = null, AddBuffInfo[] addBuffs = null)
        {
            this.id = id;
            this.name = name;
            this.tags = tags;
            this.description = description;
            this.slot = slot;
            this.propMod = new ChaProp[2] { ChaProp.zero, ChaProp.zero };
            if (propMod != null)
                for (int i = 0; i < Mathf.Min(2, propMod.Length); i++)
                    this.propMod[i] = propMod[i];
            this.addBuffs = addBuffs;
        }
    }
}