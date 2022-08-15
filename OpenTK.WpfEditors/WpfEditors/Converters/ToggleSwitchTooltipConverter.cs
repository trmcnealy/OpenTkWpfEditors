using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class ToggleSwitchToolTipConverter : IValueConverter
{
    public object? Convert(object?     value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        if(value == null)
        {
            return null;
        }

        string strValue = (string)value;
        if(strValue == "")
        {
            return null;
        }

        return strValue;
    }

    public object? ConvertBack(object?     value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
