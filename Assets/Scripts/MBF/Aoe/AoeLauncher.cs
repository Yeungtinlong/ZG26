using System.Collections.Generic;
using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    [XLua.CSharpCallLua]
    public class AoeLauncher
    {
        public AoeModel model;
        public GameObject caster;
        public int side;
        public Vector3 targetPos;
        public int duration;
        public int tickTime;
        public float radius;
        public ChaProp propWhileCast;
        public Dictionary<string, object> parameters;

        public AoeLauncher(AoeModel model,
            GameObject caster, int side, Vector3 targetPos,
            int duration, int tickTime, float radius,
            ChaProp propWhileCast,
            Dictionary<string, object> parameters = null)
        {
            this.model = model;
            this.caster = caster;
            this.side = side;
            this.targetPos = targetPos;
            this.duration = duration;
            this.tickTime = tickTime;
            this.radius = radius;
            this.propWhileCast = propWhileCast;
            this.parameters = parameters;
        }
    }
}