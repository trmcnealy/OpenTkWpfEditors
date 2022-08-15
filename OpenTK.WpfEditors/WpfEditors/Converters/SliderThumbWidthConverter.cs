//using System;
//using System.Globalization;
//using System.Windows.Controls;
//using System.Windows.Data;
//using OpenTK.WpfEditors.Controls;

//namespace OpenTK.WpfEditors.Converters;

//public class SliderThumbWidthConverter : IValueConverter
//{
//    public object? Convert(object?     value,
//                          Type        targetType,
//                          object      parameter,
//                          CultureInfo culture)
//    {
//        if(value is Slider)
//        {
//            string param = parameter.ToString();
//            if(param == "0")
//            {
//                return RangeSlider.GetThumbWidth((Slider)value);
//            }
//            else if(param == "1")
//            {
//                return RangeSlider.GetThumbHeight((Slider)value);
//            }
//        }

//        return 0d;
//    }

//    public object? ConvertBack(object?     value,
//                              Type        targetType,
//                              object      parameter,
//                              CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}
