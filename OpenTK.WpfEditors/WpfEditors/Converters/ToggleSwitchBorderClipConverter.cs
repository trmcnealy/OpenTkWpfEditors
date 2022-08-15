using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace OpenTK.WpfEditors.Converters;

public class ToggleSwitchBorderClipConverter : IMultiValueConverter
{
    public object? Convert(object[]    values,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        if(values.Length == 3 && values[0] is double && values[1] is double && values[2] is CornerRadius)
        {
            double width  = (double)values[0];
            double height = (double)values[1];

            if(width < double.Epsilon || height < double.Epsilon)
            {
                return Geometry.Empty;
            }

            CornerRadius      radius = (CornerRadius)values[2];
            RectangleGeometry clip   = new(new Rect(0, 0, width, height), radius.TopLeft, radius.TopLeft);
            clip.Freeze();

            return clip;
        }

        return DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object?     value,
                                Type[]      targetTypes,
                                object      parameter,
                                CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
