using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class TextColorConverter : IValueConverter
{
    public object? Convert(object      value,
                          Type        typeTarget,
                          object      param,
                          CultureInfo culture)
    {
        if(value is string text)
        {
            if(text.StartsWith("Exception:"))
            {
                return "Red";
            }

            return "Black";
        }

        return "Black";
    }

    public object? ConvertBack(object      value,
                              Type        typeTarget,
                              object      param,
                              CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
