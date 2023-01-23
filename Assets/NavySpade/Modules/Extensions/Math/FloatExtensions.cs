using System;
using UnityEngine;

namespace NavySpade.Modules.Extensions
{
    /// <summary>
    /// <see cref="float"/> extensions.
    /// </summary>
    public static class FloatExtensions
    {
        public static float GaussFalloff(float distance, float inRadius)
        {
            return Mathf.Clamp01(Mathf.Pow(360f, -Mathf.Pow(distance / inRadius, 2.5f) - 0.01f));
        }

        /// <summary>
        /// Is this float approximately other.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool Approximately(this float a, float other)
        {
            return Mathf.Approximately(a, other);
        }

        /// <summary>
        /// Is this float within range of other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="other"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static bool Approximately(this float x, float other, float delta)
        {
            return Math.Abs(x - other) < delta;
        }
        
        public static float Normalize(this float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        public static bool IsContain(this float value, float min, float max)
        {
            return value > min && value < max;
        }
        
        // public unsafe static float FastInvSqrt(float x)
        // {
        //     float xhalf = 0.5f * x;
        //     int i = *(int*)&x;
        //     
        //     // This constant is slightly more accurate than the common one.
        //     i = 0x5f375a86 - (i >> 1);
        //     x = *(float*)&i;
        //     x = x * (1.5f - xhalf * x * x);
        //     
        //     return x;
        // }
        //
        // public unsafe static float FastSqrt(float x)
        // {
        //     float xhalf = 0.5f * x;
        //     int i = *(int*)&x;
        //     
        //     // Da magicks.
        //     i = 0x1fbd1df5 + (i >> 1);
        //     x = *(float*)&i;
        //     
        //     // Newtons method to improve approximation.
        //     x = x * (1.5f - (xhalf * x * x));
        //     
        //     return x;
        // }
    }
}