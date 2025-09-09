using UnityEngine;

namespace MBF
{
    [XLua.LuaCallCSharp]
    public struct ChaProp
    {
        public static readonly ChaProp zero = new ChaProp();

        public int hp;
        public int atk;
        public int rng;
        public int skillSpd;
        public int speed;

        public void Zero()
        {
            this.hp = 0;
            this.atk = 0;
            this.rng = 0;
            this.skillSpd = 0;
            this.speed = 0;
        }

        public ChaProp(int hp, int atk, int rng, int skillSpd, int speed)
        {
            this.hp = hp;
            this.atk = atk;
            this.rng = rng;
            this.skillSpd = skillSpd;
            this.speed = speed;
        }

        public static ChaProp operator +(ChaProp lhs, ChaProp rhs)
        {
            return new ChaProp()
            {
                hp = lhs.hp + rhs.hp,
                atk = lhs.atk + rhs.atk,
                rng = lhs.rng + rhs.rng,
                skillSpd = lhs.skillSpd + rhs.skillSpd,
                speed = lhs.speed + rhs.speed,
            };
        }

        public static ChaProp operator -(ChaProp lhs, ChaProp rhs)
        {
            return new ChaProp()
            {
                hp = lhs.hp - rhs.hp,
                atk = lhs.atk - rhs.atk,
                rng = lhs.rng - rhs.rng,
                skillSpd = lhs.skillSpd - rhs.skillSpd,
                speed = lhs.speed - rhs.speed,
            };
        }

        public static ChaProp operator *(ChaProp lhs, int num)
        {
            return new ChaProp()
            {
                hp = lhs.hp * num,
                atk = lhs.atk * num,
                rng = lhs.rng * num,
                skillSpd = lhs.skillSpd * num,
                speed = lhs.speed * num,
            };
        }

        public static ChaProp operator *(int num, ChaProp rhs)
        {
            return rhs * num;
        }

        /// <summary>
        /// 两者相乘时，右操作数视为百分比
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static ChaProp operator *(ChaProp lhs, ChaProp rhs)
        {
            return new ChaProp()
            {
                hp = Mathf.RoundToInt(lhs.hp * (1.00f + rhs.hp * 0.01f)),
                atk = Mathf.RoundToInt(lhs.atk * (1.00f + rhs.atk * 0.01f)),
                rng = Mathf.RoundToInt(lhs.rng * (1.00f + rhs.rng * 0.01f)),
                skillSpd = Mathf.RoundToInt(lhs.skillSpd * (1.00f + rhs.skillSpd * 0.01f)),
                speed = Mathf.RoundToInt(lhs.speed * (1.00f + rhs.speed * 0.01f)),
            };
        }
    }
}