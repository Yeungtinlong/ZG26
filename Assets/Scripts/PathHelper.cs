using System;
using MBF;

namespace TheGame
{
    public static class PathHelper
    {
        public const string k_ItemImagesPath = "Images/UI_Item_{0}";
        public static string GetItemImagePath(string itemName) => string.Format(k_ItemImagesPath, itemName);
        
        public const string k_PrefabPath = "Prefabs/{0}.prefab";
        public static string GetPrefabPath(string prefabName) => string.Format(k_PrefabPath, prefabName);
    }
}