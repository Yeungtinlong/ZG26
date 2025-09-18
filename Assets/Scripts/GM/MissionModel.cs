using System.Collections.Generic;
using MBF;

namespace TheGame.GM
{
    [XLua.CSharpCallLua]
    public delegate bool MissionCanComplete();

    [XLua.LuaCallCSharp]
    [XLua.CSharpCallLua]
    public struct MissionModel
    {
        public string id;
        public string name;
        public string description;
        public string icon;
        public MissionCanComplete canComplete;
        public List<ItemStack> showRewards;
        public TimelineNode onClaim;
    }
}