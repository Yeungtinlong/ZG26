using System.Collections.Generic;
using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public class SkillObj
    {
        public SkillModel model;
        public int grade;
        public int cooldown;
        
        public object mainTarget;
        public List<object> targets;
        
        public Dictionary<string, object> parameters;

        public SkillObj(SkillModel model, int grade)
        {
            this.model = model;
            this.grade = grade;
            this.cooldown = 0;
            this.mainTarget = null;
            this.targets = new List<object>();
            this.parameters = new Dictionary<string, object>();
        }
    }
}