using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public struct AddBuffInfo
    {
        public BuffModel model;
        public GameObject caster;
        public int duration;
        public bool permanent;

        public AddBuffInfo(BuffModel model, GameObject caster, int duration, bool permanent)
        {
            this.model = model;
            this.caster = caster;
            this.duration = duration;
            this.permanent = permanent;
        }
    }
}