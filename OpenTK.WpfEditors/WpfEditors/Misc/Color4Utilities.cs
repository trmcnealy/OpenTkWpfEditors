using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using OpenTK.Mathematics;

namespace OpenTK.WpfEditors;

internal static class Color4Utilities
{
    public static readonly Dictionary<string, Color4?> KnownColors = GetKnownColors();

    public static string GetColorName(this Color4 color)
    {
        string colorName = KnownColors.Where(kvp => kvp.Value.Equals(color)).Select(kvp => kvp.Key).FirstOrDefault() ?? string.Empty;

        if(string.IsNullOrEmpty(colorName))
        {
            colorName = color.ToString();
        }

        return colorName;
    }

    public static string FormatColorString(string stringToFormat,
                                           bool   isUsingAlphaChannel)
    {
        if(!isUsingAlphaChannel && stringToFormat.Length == 9)
        {
            return stringToFormat.Remove(1, 2);
        }

        return stringToFormat;
    }

    private static Dictionary<string, Color4?> GetKnownColors()
    {
        PropertyInfo[] colorProperties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
        return colorProperties.ToDictionary(p => p.Name, p => (Color4?)p.GetValue(null, null));
    }

    /// <summary>
    /// Converts an RGB color to an HSV color.
    /// </summary>
    /// <param name="r"></param>
    /// <param name="b"></param>
    /// <param name="g"></param>
    /// <returns></returns>
    public static HsvColor4 ConvertRgbToHsv(float r,
                                            float b,
                                            float g,
                                            float a)
    {
        float delta;
        float min;

        float h = 0;
        float s;
        float v;

        min   = MathF.Min(MathF.Min(r, g), b);
        v     = MathF.Max(MathF.Max(r, g), b);
        delta = v - min;

        if(v == 0.0)
        {
            s = 0;
        }
        else
        {
            s = delta / v;
        }

        if(s == 0)
        {
            h = 0.0f;
        }

        else
        {
            if(MathF.Abs(r - v) < float.Epsilon)
            {
                h = (g - b) / delta;
            }
            else if(MathF.Abs(g - v) < float.Epsilon)
            {
                h = 2 + (b - r) / delta;
            }
            else if(MathF.Abs(b - v) < float.Epsilon)
            {
                h = 4 + (r - g) / delta;
            }

            h *= 60;
            if(h < 0.0)
            {
                h = h + 360;
            }
        }

        return new HsvColor4
        {
            H = h,
            S = s,
            V = v / 255f,
            A = a
        };
    }

    /// <summary>
    ///  Converts an HSV color to an RGB color.
    /// </summary>
    /// <param name="h"></param>
    /// <param name="s"></param>
    /// <param name="v"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Color4 ConvertHsvToRgb(float h,
                                         float s,
                                         float v,
                                         float a)
    {
        float r = 0f;
        float g = 0f;
        float b = 0f;

        if(s == 0f)
        {
            r = v;
            g = v;
            b = v;
        }
        else
        {
            if(MathF.Abs(h - 360f) < float.Epsilon)
            {
                h = 0f;
            }
            else
            {
                h = h / 60f;
            }

            int   i = (int)MathF.Truncate(h);
            float f = h - i;

            float p = v * (1.0f - s);
            float q = v * (1.0f - s * f);
            float t = v * (1.0f - s * (1.0f - f));

            switch(i)
            {
                case 0:
                {
                    r = v;
                    g = t;
                    b = p;
                    break;
                }
                case 1:
                {
                    r = q;
                    g = v;
                    b = p;
                    break;
                }
                case 2:
                {
                    r = p;
                    g = v;
                    b = t;
                    break;
                }
                case 3:
                {
                    r = p;
                    g = q;
                    b = v;
                    break;
                }
                case 4:
                {
                    r = t;
                    g = p;
                    b = v;
                    break;
                }
                default:
                {
                    r = v;
                    g = p;
                    b = q;
                    break;
                }
            }
        }

        return new Color4(r, g, b, a);
    }

    /// <summary>
    /// Generates a list of colors with hues ranging from 0 360 and a saturation and value of 1. 
    /// </summary>
    /// <returns></returns>
    public static List<Color4> GenerateHsvSpectrum()
    {
        List<Color4> colorsList = new(8);

        for(int i = 0; i < 29; i++)
        {
            colorsList.Add(ConvertHsvToRgb(i * 12f, 1f, 1f, 1f));
        }

        colorsList.Add(ConvertHsvToRgb(0f, 1f, 1f, 1f));

        return colorsList;
    }
}
