using System.Collections.Generic;
using Common;
using TheGame;
using UnityEngine;

namespace MBF
{
    [XLua.CSharpCallLua]
    public delegate GameObject BulletTargeting(BulletState bs, List<GameObject> targets);

    [XLua.CSharpCallLua]
    public delegate Vector3 BulletTween(float t, BulletState bs, GameObject target);

    [XLua.LuaCallCSharp]
    public class BulletLauncher
    {
        public BulletModel model;
        public GameObject caster;
        public int side;
        public Vector3 launchPos;
        public Vector3 targetPos;
        public Vector3 launchDir;
        public float radius;
        public float speed;
        public int duration;
        public int hp;
        public bool justHitTarget;

        public BulletTargeting targeting;
        public BulletTween tween;

        public ChaProp propWhileCast;
        public Dictionary<string, object> parameters;

        public BulletLauncher(BulletModel model,
            GameObject caster, int side,
            Vector3 launchPos, Vector3 targetPos, Vector3 launchDir,
            float radius, float speed, int duration, int hp, bool justHitTarget,
            BulletTargeting targeting, BulletTween tween,
            ChaProp propWhileCast,
            Dictionary<string, object> parameters = null)
        {
            this.model = model;
            this.caster = caster;
            this.side = side;
            this.launchPos = launchPos;
            this.targetPos = targetPos;
            this.launchDir = launchDir;
            this.radius = radius;
            this.speed = speed;
            this.duration = duration;
            this.hp = hp;
            this.justHitTarget = justHitTarget;
            this.targeting = targeting;
            this.tween = tween;
            this.propWhileCast = propWhileCast;
            this.parameters = parameters;
        }
    }
}