using System;
using MBF;

namespace TheGame
{
    [Serializable]
    public class ChaInstance
    {
        public static int EQUIP_LIMIT => Enum.GetValues(typeof(EquipmentSlot)).Length;
        
        public string id;
        public int grade;
        public bool owned;
        public string[] equipments = new string[EQUIP_LIMIT];
    }
}