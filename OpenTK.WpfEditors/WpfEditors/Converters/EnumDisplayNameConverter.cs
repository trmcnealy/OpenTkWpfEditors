//using System;
//using System.ComponentModel;
//using System.Globalization;
//using System.Linq;
//using System.Reflection;
//using OpenTK.WpfEditors.Controls;

//namespace OpenTK.WpfEditors.Converters;

//public class EnumDisplayNameConverter : EnumConverter
//{
//    public EnumDisplayNameConverter()
//        : this(null)
//    {
//    }

//    public EnumDisplayNameConverter(Type type)
//        : base(type)
//    {
//    }

//    public override bool CanConvertTo(ITypeDescriptorContext context,
//                                      Type                   destinationType)
//    {
//        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
//    }

//    public override bool CanConvertFrom(ITypeDescriptorContext context,
//                                        Type                   sourceType)
//    {
//        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
//    }

//    public override object ConvertTo(ITypeDescriptorContext context,
//                                     CultureInfo            culture,
//                                     object?                value,
//                                     Type                   destinationType)
//    {
//        if(destinationType == typeof(string))
//        {
//            FieldInfo fieldInfo = EnumType.GetField(value.ToString());
//            if(fieldInfo != null)
//            {
//                ExtendedDisplayNameAttribute displayNameAtt = (ExtendedDisplayNameAttribute)fieldInfo.GetCustomAttributes(typeof(ExtendedDisplayNameAttribute), false).FirstOrDefault();
//                if(displayNameAtt != null)
//                {
//                    return displayNameAtt.DisplayName;
//                }
//            }
//        }

//        return base.ConvertTo(context, culture, value, destinationType);
//    }

//    public override object ConvertFrom(ITypeDescriptorContext context,
//                                       CultureInfo            currentCulture,
//                                       object?                value)
//    {
//        if(value != null && value is string)
//        {
//            FieldInfo[] fieldInfos = EnumType.GetFields();
//            foreach(FieldInfo fieldInfo in fieldInfos)
//            {
//                ExtendedDisplayNameAttribute displayNameAtt = (ExtendedDisplayNameAttribute)fieldInfo.GetCustomAttributes(typeof(ExtendedDisplayNameAttribute), false).FirstOrDefault();
//                if(displayNameAtt != null)
//                {
//                    if(Equals((string)value, displayNameAtt.DisplayName))
//                    {
//                        return fieldInfo.GetValue(fieldInfo.Name);
//                    }
//                }
//            }
//        }

//        return base.ConvertFrom(context, currentCulture, value);
//    }
//}
