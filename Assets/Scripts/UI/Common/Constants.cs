using System;
using MBF;

namespace TheGame.UI
{
    public static class Constants
    {
        public static int EQUIP_LIMIT => Enum.GetValues(typeof(EquipmentSlot)).Length;
        public static readonly ItemStack SUMMON_ONCE_PRICE = new ItemStack("元宝", 100);
        
        public const string ItemImagesPath = "Images/UI_Item_{0}";
        public static string GetItemImagePath(string itemName) => string.Format(ItemImagesPath, itemName);
        
        public const string PrefabPath = "Prefabs/{0}.prefab";
        public static string GetPrefabPath(string prefabName) => string.Format(PrefabPath, prefabName);
    }
}