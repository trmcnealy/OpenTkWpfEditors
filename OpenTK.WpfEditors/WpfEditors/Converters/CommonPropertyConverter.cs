//using System;
//using System.ComponentModel;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Windows.Data;

//namespace OpenTK.WpfEditors.Converters;

//public class CommonPropertyConverter : IMultiValueConverter
//{
//    #region Members

//    private TypeConverter _propertyTypeConverter;

//    #endregion

//    #region Constructors

//    internal CommonPropertyConverter(Type type)
//    {
//        _propertyTypeConverter = TypeDescriptor.GetConverter(type);
//    }

//    #endregion

//    #region IMultiValueConverter Members

//    public object? Convert(object[]    values,
//                          Type        targetType,
//                          object      parameter,
//                          CultureInfo culture)
//    {
//        // If there is a difference in all the values, do not display anything.
//        if(values.Distinct().Count() > 1)
//        {
//            return null;
//        }

//        return values[0];
//    }

//    public object[] ConvertBack(object?     value,
//                                Type[]      targetTypes,
//                                object      parameter,
//                                CultureInfo culture)
//    {
//        object data = value;
//        if(Controls.GeneralUtilities.CanConvertValue(value, targetTypes[0]))
//        {
//            if(!_propertyTypeConverter.CanConvertFrom(value.GetType()))
//            {
//                throw new InvalidDataException("Cannot convert from targetType.");
//            }

//            data = _propertyTypeConverter.ConvertFrom(value);
//        }

//        // All the values will receive the new value.
//        object[] values = Enumerable.Repeat(data, targetTypes.Count()).ToArray();
//        return values;
//    }

//    #endregion
//}
