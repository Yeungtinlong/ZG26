using MBF;

namespace TheGame
{
    [XLua.CSharpCallLua]
    public delegate bool SummonCondition(LSummonConfig summonConfig);
    
    [XLua.CSharpCallLua]
    public interface LSummonConfig
    {
        public string Id { get; set; }
        public int Weight { get; set; }
        public SummonCondition Condition { get; set; }
        public TimelineNode Effect { get; set; }
    }
}