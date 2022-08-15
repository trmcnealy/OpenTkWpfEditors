using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class StylableWindowClippingBorderConverter : IValueConverter
{
    public object? Convert(object?     value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        Type   aa              = value.GetType();
        Type   b               = parameter.GetType();
        double borderThickness = (double)value - double.Parse((string)parameter);
        return new Thickness(borderThickness, borderThickness, borderThickness, borderThickness);
    }

    public object? ConvertBack(object?     value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
