using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class SelectedObjectConverter : IValueConverter
{
    private const string ValidParameterMessage = @"parameter must be one of the following strings: 'Type', 'TypeName', 'SelectedObjectName'";

    #region IValueConverter Members

    public object? Convert(object?     value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        if(parameter == null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }

        if(!(parameter is string))
        {
            throw new ArgumentException(ValidParameterMessage);
        }

        if(CompareParam(parameter, "Type"))
        {
            return ConvertToType(value, culture);
        }
        else if(CompareParam(parameter, "TypeName"))
        {
            return ConvertToTypeName(value, culture);
        }
        else if(CompareParam(parameter, "SelectedObjectName"))
        {
            return ConvertToSelectedObjectName(value, culture);
        }
        else
        {
            throw new ArgumentException(ValidParameterMessage);
        }
    }

    private bool CompareParam(object parameter,
                              string parameterValue)
    {
        return string.Compare((string)parameter, parameterValue, true) == 0;
    }

    private object ConvertToType(object?     value,
                                 CultureInfo culture)
    {
        return value != null ? value.GetType() : null;
    }

    private object ConvertToTypeName(object?     value,
                                     CultureInfo culture)
    {
        if(value == null)
        {
            return string.Empty;
        }

        Type newType = value.GetType();

        DisplayNameAttribute displayNameAttribute = newType.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault();

        return displayNameAttribute == null ? newType.Name : displayNameAttribute.DisplayName;
    }

    private object ConvertToSelectedObjectName(object?     value,
                                               CultureInfo culture)
    {
        if(value == null)
        {
            return string.Empty;
        }

        Type           newType    = value.GetType();
        PropertyInfo[] properties = newType.GetProperties();
        foreach(PropertyInfo property in properties)
        {
            if(property.Name == "Name")
            {
                return property.GetValue(value, null);
            }
        }

        return string.Empty;
    }

    public object? ConvertBack(object?     value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    #endregion
}
