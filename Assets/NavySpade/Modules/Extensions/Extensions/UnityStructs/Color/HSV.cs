using UnityEngine;

/// <summary>
/// HSV represents color in a cylinder and also contains three separate channels but each one as a completely different meaning.
/// Hue: First channel defines the color itself as an angle on a color cylinder.
/// Saturation: Second channel defines amount of gray in the color as a distance across a color cylinder radius.
/// Value: Third channel defines brightness/intensity of the color as a distance across a color cylinder height.
/// </summary>
public struct HSV
{
    /// <summary>
    /// This is actually a 'degree'. ranges from 0 - 360.
    /// </summary>
    private float _hue;

    public float Hue
    {
        get => _hue;
        set => _hue = Mathf.Clamp(value, 0, 360f);
    }

    /// <summary>
    /// 0-1. if 0, then h = -1 (undefined) 
    /// </summary>
    private float _saturation;

    public float Saturation
    {
        get => _saturation;
        set => _saturation = Mathf.Clamp01(value);
    }

    private float _value;

    public float Value
    {
        get => _value;
        set => _value = Mathf.Clamp01(value);
    }

    public Color GetColor()
    {
        float r, g, b;
        var h = _hue;
        var s = _saturation;
        var v = _value;

        if (s == 0)
        {
            // Achromatic (grey)
            r = g = b = v;
            return new Color(r, g, b, 1f);
        }

        // Sector 0 to 5
        h /= 60f;
        var i = (int)h;
        var f = h - i;
        var p = v * (1f - s);
        var q = v * (1f - s * f);
        var t = v * (1f - s * (1f - f));

        switch (i)
        {
            case 0:
                r = v;
                g = t;
                b = p;
                break;
            case 1:
                r = q;
                g = v;
                b = p;
                break;
            case 2:
                r = p;
                g = v;
                b = t;
                break;
            case 3:
                r = p;
                g = q;
                b = v;
                break;
            case 4:
                r = t;
                g = p;
                b = v;
                break;
            default: // case 5:
                r = v;
                g = p;
                b = q;
                break;
        }

        return new Color(r, g, b, 1f);
    }

    public static HSV FromColor(Color color)
    {
        float s, h;

        var min = Mathf.Min(color.r, color.g, color.b);
        var max = Mathf.Max(color.r, color.g, color.b);

        var delta = max - min;

        if (max != 0)
        {
            s = delta / max;
        }
        else
        {
            // r = g = b = 0
            // s = 0, v is undefined
            s = 0;
            h = -1f;
            return new HSV() { Hue = h, Saturation = s, Value = max };
        }

        if (Mathf.Approximately(color.r, max))
        {
            // Between yellow & magenta.
            h = (color.g - color.b) / delta;
        }
        else if (Mathf.Approximately(color.g, max))
        {
            // Between cyan & yellow.
            h = 2f + (color.b - color.r) / delta;
        }
        else
        {
            // Between magenta & cyan.
            h = 4f + (color.r - color.g) / delta;
        }

        h *= 60f; // degrees
        if (h < 0)
        {
            h += 360f;
        }

        return new HSV() { Hue = h, Saturation = s, Value = max };
    }
}