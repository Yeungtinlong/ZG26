using System.Collections.Generic;
using TheGame;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public struct TrapModel
    {
        public string id;
        public string name;
        public List<ItemStack> materials;
        public TimelineModel timeline;
    }
}