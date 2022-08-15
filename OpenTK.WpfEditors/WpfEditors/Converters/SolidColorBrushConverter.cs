using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace OpenTK.WpfEditors.Converters;

public class SolidColorBrushConverter : IValueConverter
{
    public object? Convert(object      value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        switch(value)
        {
            case null:
                return null;
            case Color color:
                return new SolidColorBrush(color);
            default:
            {
                Type type = value.GetType();

                throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
            }
        }
    }

    public object? ConvertBack(object      value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
