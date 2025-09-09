using System;
using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public struct BuffModel
    {
        public string id;
        public string[] tags;
        public ChaProp[] propMod;
        public ChaControlState stateMod;

        public BuffModel(string id, string[] tags, ChaControlState stateMod, ChaProp[] propMod = null)
        {
            this.id = id;
            this.tags = tags;
            this.propMod = propMod;
            this.stateMod = stateMod;
            this.propMod = new ChaProp[2] { ChaProp.zero, ChaProp.zero };
            if (propMod != null)
                for (int i = 0; i < Mathf.Min(2, propMod.Length); i++)
                    this.propMod[i] = propMod[i];
        }
    }
}