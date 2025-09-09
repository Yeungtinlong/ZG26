using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public class TimelineObj
    {
        public TimelineModel model;
        public GameObject caster;
        /// <summary>
        /// 该timeline的来源，如果是技能，那么这里就是SkillObj
        /// </summary>
        public object source;
        public int tickElapsed;
        public float timescale;

        public TimelineObj(TimelineModel model, GameObject caster, object source, float timescale = 1.0f)
        {
            this.model = model;
            this.caster = caster;
            this.source = source;
            this.tickElapsed = 0;
            this.timescale = timescale;
        }
    }
}