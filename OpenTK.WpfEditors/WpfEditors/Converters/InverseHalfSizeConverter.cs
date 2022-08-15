using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class InverseHalfSizeConverter : IValueConverter
{
    public object? Convert(object?     value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        double height     = (double)value;
        double multiplier = (string)parameter == "0" ? 1d : -1d;
        return multiplier * (height / 2);
    }

    public object? ConvertBack(object?     value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
