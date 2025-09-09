using System.Collections.Generic;

namespace MBF
{
    [XLua.CSharpCallLua]
    public delegate object TimelineEvent(TimelineObj timelineObj, object[] args);
    
    [XLua.LuaCallCSharp]
    public struct TimelineModel
    {
        public string id;
        public int duration;
        public List<TimelineNode> nodes;

        public TimelineModel(string id, int duration, List<TimelineNode> nodes)
        {
            this.id = id;
            this.duration = duration;
            this.nodes = nodes;
        }
    }
}