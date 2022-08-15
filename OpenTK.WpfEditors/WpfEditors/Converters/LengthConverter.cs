using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace OpenTK.WpfEditors.Converters;

public class LengthConverter : TypeConverter
{
    public static LengthConverter Default = new();


    public override bool CanConvertFrom(ITypeDescriptorContext? typeDescriptorContext,
                                        Type                   sourceType)
    {
        TypeCode tc = Type.GetTypeCode(sourceType);

        switch(tc)
        {
            case TypeCode.String:
            case TypeCode.Decimal:
            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            {
                return true;
            }
            default:
            {
                return false;
            }
        }
    }

    public override bool CanConvertTo(ITypeDescriptorContext? typeDescriptorContext,
                                      Type                   destinationType)
    {
        if(destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string))
        {
            return true;
        }

        return false;
    }

    public override object? ConvertFrom(ITypeDescriptorContext? typeDescriptorContext,
                                       CultureInfo?            cultureInfo,
                                       object?                source)
    {
        if(source != null)
        {
            if(source is string s)
            {
                return FromString(s, cultureInfo);
            }

            return Convert.ToDouble(source, cultureInfo);
        }

        throw GetConvertFromException("source");
    }

    public override object? ConvertTo(ITypeDescriptorContext? typeDescriptorContext,
                                     CultureInfo?            cultureInfo,
                                     object                 value,
                                     Type                   destinationType)
    {
        if(destinationType == null)
        {
            throw new ArgumentNullException(nameof(destinationType));
        }

        if(value is double l)
        {
            if(destinationType == typeof(string))
            {
                if(double.IsNaN(l))
                {
                    return "Auto";
                }

                return Convert.ToString(l, cultureInfo);
            }

            if(destinationType == typeof(InstanceDescriptor))
            {
                ConstructorInfo ci = typeof(double).GetConstructor(new Type[]
                {
                    typeof(double)
                });

                return new InstanceDescriptor(ci,
                                              new object[]
                                              {
                                                  l
                                              });
            }
        }

        throw GetConvertToException(value, destinationType);
    }

    internal static double FromString(string      s,
                                      CultureInfo cultureInfo)
    {
        string valueString = s.Trim();
        string goodString  = valueString.ToLowerInvariant();
        int    strLen      = goodString.Length;
        int    strLenUnit  = 0;
        double unitFactor  = 1.0;

        if(goodString == "auto")
        {
            return double.NaN;
        }

        for(int i = 0; i < PixelUnitStrings.Length; i++)
        {
            if(goodString.EndsWith(PixelUnitStrings[i], StringComparison.Ordinal))
            {
                strLenUnit = PixelUnitStrings[i].Length;
                unitFactor = PixelUnitFactors[i];

                break;
            }
        }

        valueString = valueString.Substring(0, strLen - strLenUnit);

        try
        {
            double result = Convert.ToDouble(valueString, cultureInfo) * unitFactor;

            return result;
        }
        catch(FormatException)
        {
            throw new FormatException($"Length Format Error: {valueString}");
        }
    }

    private static readonly string[] PixelUnitStrings =
    {
        "px", "in", "cm", "pt"
    };

    private static readonly double[] PixelUnitFactors =
    {
        1.0,         // Pixel itself
        96.0,        // Pixels per Inch
        96.0 / 2.54, // Pixels per Centimeter
        96.0 / 72.0  // Pixels per Point
    };

    internal static string ToString(double      l,
                                    CultureInfo cultureInfo)
    {
        if(double.IsNaN(l))
        {
            return "Auto";
        }

        return Convert.ToString(l, cultureInfo);
    }
}
