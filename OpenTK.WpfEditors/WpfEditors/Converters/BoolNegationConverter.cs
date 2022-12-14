using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class BoolNegationConverter : IValueConverter
{
    /// <summary>
    /// Convert a boolean value to its negation.
    /// </summary>
    /// <param name="value">The <see cref="bool"/> value to negate.</param>
    /// <param name="targetType">The type of the target property, as a type reference.</param>
    /// <param name="parameter">Optional parameter. Not used.</param>
    /// <param name="language">The language of the conversion. Not used</param>
    /// <returns>The value to be passed to the target dependency property.</returns>
    public object? Convert(object?      value,
                          Type?        targetType,
                          object?      parameter,
                          CultureInfo? culture)
    {
        return !(value is bool b && b);
    }

    /// <summary>
    /// Convert back a boolean value to its negation.
    /// </summary>
    /// <param name="value">The <see cref="bool"/> value to negate.</param>
    /// <param name="targetType">The type of the target property, as a type reference.</param>
    /// <param name="parameter">Optional parameter. Not used.</param>
    /// <param name="language">The language of the conversion. Not used</param>
    /// <returns>The value to be passed to the target dependency property.</returns>
    public object? ConvertBack(object?      value,
                              Type?        targetType,
                              object?      parameter,
                              CultureInfo? culture)
    {
        return !(value is bool b && b);
    }
}
