using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class PositionConverter : IMultiValueConverter
{
    public object? Convert(object[]    values,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        if(values.Count() == 2 && values[0] != DependencyProperty.UnsetValue && values[1] != DependencyProperty.UnsetValue)
        {
            double offsetX = (double)values[0];
            double offsetY = (double)values[1];

            return new Point(offsetX, offsetY);
        }

        return new Point(0d, 0d);
    }

    public object[] ConvertBack(object?     value,
                                Type[]      targetTypes,
                                object      parameter,
                                CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
