using System;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class CalculatorMemoryToVisibilityConverter : IValueConverter
{
    public object? Convert(object?                          value,
                          Type                             targetType,
                          object                           parameter,
                          System.Globalization.CultureInfo culture)
    {
        return (decimal)value == decimal.Zero ? Visibility.Hidden : Visibility.Visible;
    }

    public object? ConvertBack(object?                          value,
                              Type                             targetType,
                              object                           parameter,
                              System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
