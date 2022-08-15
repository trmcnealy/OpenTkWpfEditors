using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

internal class MultipleValuesConverter : IMultiValueConverter
{
    #region IMultiValueConverter Members

    public object? Convert(object[]?   values,
                           Type        targetType,
                           object      parameter,
                           CultureInfo culture)
    {
        if(values == null)
        {
            return null;
        }

        object[] vals = new object[values.Length];
        Array.Copy(values, vals, values.Length);
        return vals;
    }

    public object[]? ConvertBack(object?     value,
                                 Type[]      targetTypes,
                                 object      parameter,
                                 CultureInfo culture)
    {
        return (object[]?)value;
    }

    #endregion
}
