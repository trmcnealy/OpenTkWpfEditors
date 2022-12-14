using System;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class ObjectTypeToNameConverter : IValueConverter
{
    public object? Convert(object?                          value,
                          Type                             targetType,
                          object                           parameter,
                          System.Globalization.CultureInfo culture)
    {
        if(value != null)
        {
            string valueString = value.ToString();
            if(string.IsNullOrEmpty(valueString) || valueString == value.GetType().UnderlyingSystemType.ToString())
            {
                return value.GetType().Name;
            }

            return value;
        }

        return null;
    }

    public object? ConvertBack(object?                          value,
                              Type                             targetType,
                              object                           parameter,
                              System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
