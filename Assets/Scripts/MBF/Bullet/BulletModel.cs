using UnityEngine;

namespace MBF
{
    [XLua.CSharpCallLua]
    public delegate void BulletOnHit(BulletState bs, GameObject defender);

    [XLua.LuaCallCSharp]
    public struct BulletModel
    {
        public string id;
        public string prefab;
        public bool hitFoe;
        public bool hitAlly;
        public int sameTargetDelay;
        public BulletOnHit onHit;
        public object[] onHitParams;

        public BulletModel(string id, string prefab, bool hitFoe, bool hitAlly, int sameTargetDelay, BulletOnHit onHit, object[] onHitParams)
        {
            this.id = id;
            this.prefab = prefab;
            this.hitFoe = hitFoe;
            this.hitAlly = hitAlly;
            this.sameTargetDelay = sameTargetDelay;
            this.onHit = onHit;
            this.onHitParams = onHitParams;
        }
    }
}