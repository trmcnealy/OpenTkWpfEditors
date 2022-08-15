using System;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class BorderThicknessToStrokeThicknessConverter : IValueConverter
{
    #region IValueConverter Members

    public object? Convert(object?                          value,
                          Type                             targetType,
                          object                           parameter,
                          System.Globalization.CultureInfo culture)
    {
        Thickness? thickness = (Thickness?)value;

        if (thickness.HasValue)
        {
            return (thickness.Value.Bottom + thickness.Value.Left + thickness.Value.Right + thickness.Value.Top) / 4;
        }
        return null;
    }

    public object? ConvertBack(object?                          value,
                              Type                             targetType,
                              object                           parameter,
                              System.Globalization.CultureInfo culture)
    {
        int? thick      = (int?)value;
        int  thickValue = thick ?? 0;

        return new Thickness(thickValue, thickValue, thickValue, thickValue);
    }

    #endregion
}
