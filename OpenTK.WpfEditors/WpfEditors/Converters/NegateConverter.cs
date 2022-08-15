using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class NegateConverter : IValueConverter
{
    public object? Convert(object?     value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        double num;
        if(!double.TryParse(value.ToString(), out num))
        {
            throw new InvalidOperationException("Value is not a number.");
        }

        return -num;
    }

    public object? ConvertBack(object?     value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
