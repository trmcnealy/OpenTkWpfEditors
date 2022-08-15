#region file using directives

using System.Windows.Media;

#endregion

namespace OpenTK.WpfEditors.Converters;

/// <summary>
/// Helper class to perform such operaions on colors as convertion to/from different formats, etc.
/// </summary>
public class ColorConverter
{
    private static double Min(double r,
                              double g,
                              double b)
    {
        double result = r < g ? r : g;
        return b < result ? b : result;
    }

    private static double Max(double r,
                              double g,
                              double b)
    {
        double result = r > g ? r : g;
        return b > result ? b : result;
    }

    public static void RGBtoHSV(double     r,
                                double     g,
                                double     b,
                                out double h,
                                out double s,
                                out double v)
    {
        // r,g,b values are from 0 to 1
        // h = [0,360], s = [0,1], v = [0,1]
        // if s == 0, then h = -1 (undefined)

        r = r / 255d;
        g = g / 255d;
        b = b / 255d;
        double min,
               max,
               delta;
        min   = Min(r, g, b);
        max   = Max(r, g, b);
        v     = max; // v
        delta = max - min;
        if(max != 0)
        {
            s = delta / max; // s
        }
        else
        {
            // r = g = b = 0        // s = 0, v is undefined
            s = 0;
            h = -1;
            return;
        }

        if(r == max)
        {
            h = (g - b) / delta; // between yellow & magenta
        }
        else if(g == max)
        {
            h = 2 + (b - r) / delta; // between cyan & yellow
        }
        else
        {
            h = 4 + (r - g) / delta; // between magenta & cyan
        }

        h *= 60; // degrees

        if(h < 0)
        {
            h += 360;
        }
    }

    public static void HSVtoRGB(double     h,
                                double     s,
                                double     v,
                                out double r,
                                out double g,
                                out double b)
    {
        int i;
        double f,
               p,
               q,
               t;
        if(s == 0)
        {
            // achromatic (grey)
            r = g = b = v;
            return;
        }

        h /= 60; // sector 0 to 5
        i =  (int)h;
        f =  h - i; // factorial part of h
        p =  v * (1 - s);
        q =  v * (1 - s * f);
        t =  v * (1 - s * (1 - f));

        switch(i)
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

        r = r * 255d;
        g = g * 255d;
        b = b * 255d;
    }

    /// <summary>
    /// Calculates next palette color by start color value, index and count.
    /// </summary>
    /// <param name="color">Start color value to calculate next color relative to.</param>
    /// <param name="index">Index of next color.</param>
    /// <param name="count">Count of total colors.</param>
    /// <returns></returns>
    public static Color GetNextColor(Color color,
                                     int   index,
                                     int   count)
    {
        double h,
               s,
               v;
        RGBtoHSV(color.R, color.G, color.B, out h, out s, out v);

        double r,
               g,
               b;

        s -= s * index / count;

        HSVtoRGB(h, s, v, out r, out g, out b);

        return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
    }
}
