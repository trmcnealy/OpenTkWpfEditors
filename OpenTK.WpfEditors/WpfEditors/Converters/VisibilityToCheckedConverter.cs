using System;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class VisibilityToCheckedConverter : IValueConverter
{
    public object? Convert(object                           value,
                          Type                             targetType,
                          object                           parameter,
                          System.Globalization.CultureInfo culture)
    {
        switch(value)
        {
            case null:
                return null;
            case Visibility visibility:
                return visibility == Visibility.Visible;
            default:
            {
                Type type = value.GetType();

                throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
            }
        }
    }

    public object? ConvertBack(object                           value,
                              Type                             targetType,
                              object                           parameter,
                              System.Globalization.CultureInfo culture)
    {
        switch(value)
        {
            case null:
                return null;
            case bool visibility:
                return visibility ? Visibility.Visible : Visibility.Collapsed;
            default:
            {
                Type type = value.GetType();

                throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
            }
        }
    }
}
