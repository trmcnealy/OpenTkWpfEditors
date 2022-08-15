//using System;
//using System.Windows.Data;
//using OpenTK.WpfEditors.Controls;

//namespace OpenTK.WpfEditors.Converters;

//public class RatingIconTypeConverter : IValueConverter
//{
//    public object? Convert(object?                          value,
//                          Type                             targetType,
//                          object                           parameter,
//                          System.Globalization.CultureInfo culture)
//    {
//        string         iconToUse = "";
//        RatingIconType iconType  = (RatingIconType)value;

//        switch(iconType)
//        {
//            case RatingIconType.Star:
//                iconToUse = "Star";
//                break;
//            case RatingIconType.Circle:
//                iconToUse = "Circle";
//                break;
//            case RatingIconType.Square:
//                iconToUse = "Square";
//                break;
//            case RatingIconType.Triangle:
//                iconToUse = "Triangle";
//                break;
//            case RatingIconType.Custom:
//                iconToUse = "Custom";
//                break;
//            default:
//                iconToUse = "Star";
//                break;
//        }

//        return iconToUse;
//    }

//    public object? ConvertBack(object?                          value,
//                              Type                             targetType,
//                              object                           parameter,
//                              System.Globalization.CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}
