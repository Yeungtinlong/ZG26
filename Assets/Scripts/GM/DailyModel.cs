using System.Collections.Generic;

namespace TheGame.GM
{
    [XLua.CSharpCallLua]
    public struct DailyModel
    {
        public int day;
        public List<ItemStack> rewards;
    }
}