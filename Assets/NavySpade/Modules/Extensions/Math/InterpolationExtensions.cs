using UnityEngine;

namespace NavySpade.Modules.Extensions
{
    public static class InterpolationExtensions
    {
        public static float Lerp(float from, float to, float value)
        {
            if (value < 0.0f)
                return from;
            if (value > 1.0f)
                return to;

            return (to - from) * value + from;
        }

        public static float LerpUnclamped(float from, float to, float value)
        {
            return (1.0f - value) * from + value * to;
        }

        public static float InverseLerp(float from, float to, float value)
        {
            if (from < to)
            {
                if (value < from)
                {
                    return 0.0f;
                }

                if (value > to)
                {
                    return 1.0f;
                }
            }
            else
            {
                if (value < to)
                {
                    return 1.0f;
                }

                if (value > @from)
                {
                    return 0.0f;
                }
            }

            return (value - from) / (to - from);
        }

        public static float InverseLerpUnclamped(float from, float to, float value)
        {
            return (value - from) / (to - from);
        }

        public static float SmoothStep(float from, float to, float value)
        {
            if (value < 0.0f)
                return from;
            if (value > 1.0f)
                return to;

            value = value * value * (3.0f - 2.0f * value);
            return (1.0f - value) * from + value * to;
        }

        public static float SmoothStepUnclamped(float from, float to, float value)
        {
            value = value * value * (3.0f - 2.0f * value);
            return (1.0f - value) * from + value * to;
        }

        public static float SuperLerp(float from, float to, float from2, float to2, float value)
        {
            if (from2 < to2)
            {
                if (value < from2)
                {
                    value = from2;
                }
                else if (value > to2)
                {
                    value = to2;
                }
            }
            else
            {
                if (value < to2)
                {
                    value = to2;
                }
                else if (value > from2)
                {
                    value = from2;
                }
            }

            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }

        public static float SuperLerpUnclamped(float from, float to, float from2, float to2, float value)
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }

        public static float Hermite(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
        }

        public static float Sinerp(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
        }

        public static float Coserp(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
        }

        public static float Berp(float start, float end, float value)
        {
            value = Mathf.Clamp01(value);
            value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) +
                     value) * (1f + (1.2f * (1f - value)));
            return start + (end - start) * value;
        }

        public static float Bounce(float value)
        {
            return Mathf.Abs(Mathf.Sin(6.28f * (value + 1f) * (value + 1f)) * (1f - value));
        }

        // Test for value that is near specified float (due to floating point inprecision).
        public static bool Approx(float value, float about, float range)
        {
            return ((Mathf.Abs(value - about) < range));
        }

        /*
          CLerp - Circular Lerp - is like lerp but handles the wraparound from 0 to 360.
          This is useful when interpolating eulerAngles and the object
          crosses the 0/360 boundary.  The standard Lerp function causes the object
          to rotate in the wrong direction and looks stupid. Clerp fixes that.
        */
        public static float Clerp(float start, float end, float value)
        {
            const float min = 0.0f;
            const float max = 360.0f;

            // Half the distance between min and max.
            var half = Mathf.Abs((max - min) / 2.0f);
            float retval;
            float diff;

            if ((end - start) < -half)
            {
                diff = ((max - start) + end) * value;
                retval = start + diff;
            }
            else if ((end - start) > half)
            {
                diff = -((max - end) + start) * value;
                retval = start + diff;
            }
            else
            {
                retval = start + (end - start) * value;
            }

            // Debug.Log("Start: "  + start + "   End: " + end + "  Value: " + value + "  Half: " + half + "  Diff: " + diff + "  Retval: " + retval);
            return retval;
        }
    }
}