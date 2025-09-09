using UnityEngine;

namespace MBF
{
    public class BuffObj
    {
        public BuffModel model;
        public GameObject caster;
        public int duration;
        public bool permanent;

        public BuffObj(BuffModel model, GameObject caster, int duration, bool permanent)
        {
            this.model = model;
            this.caster = caster;
            this.duration = duration;
            this.permanent = permanent;
        }
    }
}