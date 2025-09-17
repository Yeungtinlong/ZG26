using MBF;

namespace TheGame.GM
{
    [XLua.CSharpCallLua]
    public struct DailyModel
    {
        public int day;
        public string icon;
        public string description;
        public TimelineNode effect;
    }
}