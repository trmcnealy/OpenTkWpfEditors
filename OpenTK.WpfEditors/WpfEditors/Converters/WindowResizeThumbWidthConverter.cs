using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

/// <summary>
/// Convert Width (or Height) of resize thumb to values of WindowControl.WindowBorderThickness + ContentBorderOffset (defined in XAML).
/// </summary>
public class WindowResizeThumbWidthConverter : IMultiValueConverter
{
    public object? Convert(object[]    values,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        if(values[0] != null && values[0] != DependencyProperty.UnsetValue && values[1] != null && values[1] != DependencyProperty.UnsetValue)
        {
            double windowBorderThickness = (double)values[0];
            double contentBorderOffset   = (double)values[1];
            return windowBorderThickness + contentBorderOffset;
        }

        return 0d;
    }

    public object[] ConvertBack(object?     value,
                                Type[]      targetTypes,
                                object      parameter,
                                CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
