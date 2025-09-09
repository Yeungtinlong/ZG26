using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public enum DamageInfoTag
    {
        DirectHurt,
        DirectHeal
    }

    /// <summary>
    /// 伤害值，包含几种伤害类型，可以是物理伤害、法术伤害等
    /// </summary>
    [XLua.LuaCallCSharp]
    public struct Damage
    {
        public int bullet;
    }
    
    [XLua.LuaCallCSharp]
    public class DamageInfo
    {
        public GameObject attacker;
        public GameObject defender;
        public Damage damage;
        public DamageInfoTag[] tags;

        public DamageInfo(GameObject attacker, GameObject defender, Damage damage, DamageInfoTag[] tags)
        {
            this.attacker = attacker;
            this.defender = defender;
            this.damage = damage;
            this.tags = tags;
        }
    }
}