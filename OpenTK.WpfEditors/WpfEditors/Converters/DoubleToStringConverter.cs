#nullable enable
using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

[ValueConversion(typeof(double), typeof(string))]
public class DoubleToStringConverter : IValueConverter
{
    public static DoubleToStringConverter Instance { get; } = new();

    public DoubleToStringConverter()
    {
    }

    public object? Convert(object?     value,
                           Type        targetType,
                           object      parameter,
                           CultureInfo culture)
    {
        return value is double doubleValue ? doubleValue.ToString(culture) : default;
    }

    public object? ConvertBack(object?     value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        return value is string stringValue && double.TryParse(stringValue, NumberStyles.Float | NumberStyles.AllowThousands, culture, out double result) ? result : default;
    }
}
