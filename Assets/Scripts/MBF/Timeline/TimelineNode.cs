namespace MBF
{
    [XLua.LuaCallCSharp]
    public struct TimelineNode
    {
        public int tickElapsed;
        public TimelineEvent doEvent;
        public object[] eventParams;

        public TimelineNode(int tickElapsed, TimelineEvent doEvent, object[] eventParams)
        {
            this.tickElapsed = tickElapsed;
            this.doEvent = doEvent;
            this.eventParams = eventParams;
        }
    }
}