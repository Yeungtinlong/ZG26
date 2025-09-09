using System.Linq;
using UnityEngine;

namespace SupportUtils
{
    public static class MathUtils
    {
        public static Vector2 Rotate(this Vector2 v, float degree)
        {
            float cosA = Mathf.Cos(Mathf.Deg2Rad * degree);
            float sinA = Mathf.Sin(Mathf.Deg2Rad * degree);
            return new Vector2(v.x * cosA - v.y * sinA, v.x * sinA + v.y * cosA);
        }

        public static float MapTo(this float value, float min, float max, float minPrime, float maxPrime)
        {
            return (value - min) * (maxPrime - minPrime) / (max - min) + minPrime;
        }

        public static double Sqr(this double value) => value * value;
        public static float Sqr(this float value) => value * value;

        public static double ToDeg(this double radians) => radians * 180 / Mathf.PI;
        public static double ToRad(this double degrees) => degrees * Mathf.Deg2Rad;
    }
}