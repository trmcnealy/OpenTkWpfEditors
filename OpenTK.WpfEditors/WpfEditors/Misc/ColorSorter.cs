using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK.Mathematics;

namespace OpenTK.WpfEditors;

internal class ColorSorter : IComparer<Color4Item>,
                             IComparer
{
    public int Compare(Color4Item? firstItem,
                       Color4Item? secondItem)
    {
        if(firstItem == null || secondItem == null)
        {
            return -1;
        }

        Color4Item colorItem1 = firstItem;
        Color4Item colorItem2 = secondItem;

        Color4 drawingColor1 = colorItem1.Color;
        Color4 drawingColor2 = colorItem2.Color;

        // Compare Hue
        float hueColor1 = MathF.Round(drawingColor1.GetHue(), 3);
        float hueColor2 = MathF.Round(drawingColor2.GetHue(), 3);

        if(hueColor1 > hueColor2)
        {
            return 1;
        }

        if(hueColor1 < hueColor2)
        {
            return -1;
        }

        // Hue is equal, compare Saturation
        float satColor1 = MathF.Round(drawingColor1.GetSaturation(), 3);
        float satColor2 = MathF.Round(drawingColor2.GetSaturation(), 3);

        if(satColor1 > satColor2)
        {
            return 1;
        }

        if(satColor1 < satColor2)
        {
            return -1;
        }

        // Saturation is equal, compare Brightness
        float brightColor1 = MathF.Round(drawingColor1.GetBrightness(), 3);
        float brightColor2 = MathF.Round(drawingColor2.GetBrightness(), 3);

        if(brightColor1 > brightColor2)
        {
            return 1;
        }

        if(brightColor1 < brightColor2)
        {
            return -1;
        }

        return 0;
    }

    public int Compare(object? firstItem,
                       object? secondItem)
    {
        if(firstItem == null || secondItem == null)
        {
            return -1;
        }

        Color4Item colorItem1 = (Color4Item)firstItem;
        Color4Item colorItem2 = (Color4Item)secondItem;

        Color4 drawingColor1 = colorItem1.Color;
        Color4 drawingColor2 = colorItem2.Color;

        // Compare Hue
        float hueColor1 = MathF.Round(drawingColor1.GetHue(), 3);
        float hueColor2 = MathF.Round(drawingColor2.GetHue(), 3);

        if(hueColor1 > hueColor2)
        {
            return 1;
        }

        if(hueColor1 < hueColor2)
        {
            return -1;
        }

        // Hue is equal, compare Saturation
        float satColor1 = MathF.Round(drawingColor1.GetSaturation(), 3);
        float satColor2 = MathF.Round(drawingColor2.GetSaturation(), 3);

        if(satColor1 > satColor2)
        {
            return 1;
        }

        if(satColor1 < satColor2)
        {
            return -1;
        }

        // Saturation is equal, compare Brightness
        float brightColor1 = MathF.Round(drawingColor1.GetBrightness(), 3);
        float brightColor2 = MathF.Round(drawingColor2.GetBrightness(), 3);

        if(brightColor1 > brightColor2)
        {
            return 1;
        }

        if(brightColor1 < brightColor2)
        {
            return -1;
        }

        return 0;
    }
}
