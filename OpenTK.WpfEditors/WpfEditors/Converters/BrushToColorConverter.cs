using System;
using System.Globalization;
using System.Windows.Media;

namespace OpenTK.WpfEditors.Converters;

public class BrushToColorConverter : ValueConverter<Brush, Color>
{
    protected override Color Convert(Brush       brush,
                                     object      parameter,
                                     CultureInfo culture)
    {
        if(brush is SolidColorBrush solidColorBrush)
        {
            return solidColorBrush.Color;
        }

        GradientBrush gradientBrush = brush as GradientBrush;

        if(gradientBrush == null)
        {
            return Colors.Transparent;
        }

        if(gradientBrush.GradientStops.Count > 0)
        {
            return gradientBrush.GradientStops[0].Color;
        }

        return Colors.Transparent;
    }
}
