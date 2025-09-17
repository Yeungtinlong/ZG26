using MBF;

namespace TheGame.GM
{
    [XLua.LuaCallCSharp]
    public struct StrategyModel
    {
        public string id;
        public string name;
        public string description;
        public TimelineNode effect;
    }
}