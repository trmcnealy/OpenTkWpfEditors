//using System;
//using System.ComponentModel;
//using System.ComponentModel.Design.Serialization;
//using System.Globalization;
//using System.Reflection;
//using OpenTK.WpfEditors.Controls;
//using OpenTK.WpfEditors.Controls.Panels;

//namespace OpenTK.WpfEditors.Converters;

//public class StackLengthConverter : TypeConverter
//{
//    #region Static Fields

//    private static string[] PixelUnitStrings =
//    {
//        "px", "in", "cm", "pt"
//    };

//    private static double[] PixelUnitFactors =
//    {
//        1d,          // Pixel itself
//        96d,         // Pixels per Inch
//        96d / 2.54d, // Pixels per Centimeter
//        96d / 72d    // Pixels per Point
//    };

//    #endregion

//    public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptorContext,
//                                        Type                   sourceType)
//    {
//        // We can only handle strings, integral and floating types
//        TypeCode tc = Type.GetTypeCode(sourceType);
//        switch(tc)
//        {
//            case TypeCode.String:
//            case TypeCode.Decimal:
//            case TypeCode.Single:
//            case TypeCode.Double:
//            case TypeCode.Int16:
//            case TypeCode.Int32:
//            case TypeCode.Int64:
//            case TypeCode.UInt16:
//            case TypeCode.UInt32:
//            case TypeCode.UInt64:
//                return true;
//            default:
//                return false;
//        }
//    }

//    public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext,
//                                      Type                   destinationType)
//    {
//        return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string);
//    }

//    public override object ConvertFrom(ITypeDescriptorContext typeDescriptorContext,
//                                       CultureInfo            cultureInfo,
//                                       object                 source)
//    {
//        if(source == null)
//        {
//            throw GetConvertFromException(source);
//        }

//        if(source is string)
//        {
//            return FromString((string)source, cultureInfo);
//        }

//        // conversion from numeric type
//        double value;
//        bool   isAuto = false;

//        value = Convert.ToDouble(source, cultureInfo);

//        if(DoubleHelper.IsNaN(value))
//        {
//            // this allows for conversion from Width / Height = "Auto" 
//            value  = 1d;
//            isAuto = true;
//        }

//        return isAuto ? StackLength.Auto : new StackLength(value);
//    }

//    public override object ConvertTo(ITypeDescriptorContext typeDescriptorContext,
//                                     CultureInfo            cultureInfo,
//                                     object?                value,
//                                     Type                   destinationType)
//    {
//        if(destinationType == null)
//        {
//            throw new ArgumentNullException(nameof(destinationType));
//        }

//        if(value != null && value is StackLength)
//        {
//            StackLength sl = (StackLength)value;

//            if(destinationType == typeof(string))
//            {
//                return ToString(sl, cultureInfo);
//            }

//            if(destinationType == typeof(InstanceDescriptor))
//            {
//                ConstructorInfo ci = typeof(StackLength).GetConstructor(new Type[]
//                {
//                    typeof(double), typeof(bool)
//                });
//                return new InstanceDescriptor(ci,
//                                              new object[]
//                                              {
//                                                  sl.Value, sl.IsAuto
//                                              });
//            }
//        }

//        throw GetConvertToException(value, destinationType);
//    }

//    internal static string ToString(StackLength sl,
//                                    CultureInfo cultureInfo)
//    {
//        if(sl.IsAuto)
//        {
//            return "Auto";
//        }

//        return Convert.ToString(sl.Value, cultureInfo);
//    }

//    internal static StackLength FromString(string      s,
//                                           CultureInfo cultureInfo)
//    {
//        double value;
//        bool   isAuto;
//        bool   isDefault;

//        FromString(s, cultureInfo, out value, out isAuto, out isDefault);
//        StackLength result = StackLength.Default;

//        if(isAuto)
//        {
//            result = StackLength.Auto;
//        }
//        else if(!isDefault)
//        {
//            result = new StackLength(value);
//        }

//        return result;
//    }

//    private static void FromString(string      s,
//                                   CultureInfo cultureInfo,
//                                   out double  value,
//                                   out bool    isAuto,
//                                   out bool    isDefault)
//    {
//        string goodString = s.Trim().ToLowerInvariant();

//        value     = 0d;
//        isAuto    = false;
//        isDefault = false;

//        if(goodString.CompareTo("auto") == 0)
//        {
//            value  = 1d;
//            isAuto = true;
//        }

//        if(goodString.CompareTo("default") == 0)
//        {
//            value     = -1d;
//            isDefault = true;
//        }

//        // Check for setting -1 directly, this means default.
//        // Works around issue with Visual Studio designer asking for -1.
//        double result;
//        if(double.TryParse(goodString, out result))
//        {
//            if(result == -1d)
//            {
//                isDefault = true;
//                value     = -1d;
//            }
//        }

//        if(!isAuto && !isDefault)
//        {
//            int    strLen     = goodString.Length;
//            int    strLenUnit = 0;
//            double unitFactor = 1d;

//            for(int i = 0; i < PixelUnitStrings.Length; ++i)
//            {
//                if(goodString.EndsWith(PixelUnitStrings[i], StringComparison.Ordinal))
//                {
//                    strLenUnit = PixelUnitStrings[i].Length;
//                    unitFactor = PixelUnitFactors[i];
//                    break;
//                }
//            }

//            string valueString = goodString.Substring(0, strLen - strLenUnit);
//            value = Convert.ToDouble(valueString, cultureInfo) * unitFactor;
//        }
//    }
//}
