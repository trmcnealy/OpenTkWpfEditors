using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class ValueConverter<TSource, TTarget> : IValueConverter
{
    public object? Convert(object?     value,
                           Type        targetType,
                           object      parameter,
                           CultureInfo culture)
    {
        if(value == BindingOperations.DisconnectedSource)
        {
            return value;
        }

        if(!(value is TSource) && (value != null || typeof(TSource).IsValueType))
        {
            throw new ArgumentException("value", nameof(Convert));
        }

        if(!targetType.IsAssignableFrom(typeof(TTarget)))
        {
            throw new InvalidOperationException("Convert");
        }

        return Convert((TSource?)value, parameter, culture);
    }

    public object? ConvertBack(object?     value,
                               Type        targetType,
                               object      parameter,
                               CultureInfo culture)
    {
        if(!(value is TTarget) && (value != null || typeof(TTarget).IsValueType))
        {
            throw new ArgumentException("value", nameof(ConvertBack));
        }

        if(!targetType.IsAssignableFrom(typeof(TSource)))
        {
            throw new InvalidOperationException("ConvertBack");
        }

        return ConvertBack((TTarget?)value, parameter, culture);
    }

    protected virtual TTarget? Convert(TSource?    value,
                                       object?     parameter,
                                       CultureInfo culture)
    {
        throw new InvalidOperationException("Convert");
    }

    protected virtual TSource? ConvertBack(TTarget?    value,
                                           object?     parameter,
                                           CultureInfo culture)
    {
        throw new InvalidOperationException("ConvertBack");
    }
}
