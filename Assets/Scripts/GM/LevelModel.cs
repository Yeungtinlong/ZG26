using System.Collections.Generic;

namespace TheGame.GM
{
    [XLua.CSharpCallLua]
    public struct LevelModel
    {
        public int id;
        public string name;
        public List<ItemStack> rewards;
        public List<MapGridInfo> gridInfos;
        public string mapId;
    }
}