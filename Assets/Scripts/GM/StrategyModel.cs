using MBF;

namespace TheGame.GM
{
    [XLua.CSharpCallLua]
    public delegate bool StrategyUnlockCondition();
    
    [XLua.LuaCallCSharp]
    public struct StrategyModel
    {
        public string id;
        public string name;
        public string description;
        public StrategyUnlockCondition unlockCondition;
        public string unlockDescription;
        public TimelineNode effect;
    }
}