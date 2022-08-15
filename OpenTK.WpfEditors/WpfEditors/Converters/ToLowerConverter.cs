﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

/// <summary>
/// Converts string values to lower case.
/// </summary>
public class ToLowerConverter : IValueConverter
{
    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
    public object? Convert(object      value,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        if(value != null)
        {
            string? strValue = value.ToString();


            return strValue.ToLowerInvariant();
        }

        return null;
    }

    /// <summary>
    /// Converts a value.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>
    /// A converted value. If the method returns null, the valid null value is used.
    /// </returns>
    public object? ConvertBack(object      value,
                              Type        targetType,
                              object      parameter,
                              CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
