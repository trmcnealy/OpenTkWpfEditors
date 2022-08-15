//using System;
//using System.Globalization;
//using System.Windows;
//using System.Windows.Data;
//using OpenTK.WpfEditors.Controls;

//namespace OpenTK.WpfEditors.Converters;

///// <summary>
///// Convert margin of resize thumb to values of WindowControl.WindowBorderThickness, ContentBorderOffset (defined in XAML), and 0d. 
///// </summary>
//public class WindowResizeThumbMarginConverter : IMultiValueConverter
//{
//    public object? Convert(object[]    values,
//                          Type        targetType,
//                          object      parameter,
//                          CultureInfo culture)
//    {
//        double left   = 0d;
//        double right  = 0d;
//        double top    = 0d;
//        double bottom = 0d;

//        if(values[0] != null && values[0] != DependencyProperty.UnsetValue && values[1] != null && values[1] != DependencyProperty.UnsetValue)
//        {
//            WindowResizeThumb thumbID               = (WindowResizeThumb)parameter;
//            double            windowBorderThickness = (double)values[0];
//            double            contentBorderOffset   = (double)values[1];

//            switch(thumbID)
//            {
//                case WindowResizeThumb.Left:
//                case WindowResizeThumb.Right:
//                    left   = thumbID == WindowResizeThumb.Left ? -windowBorderThickness : 0d;
//                    right  = thumbID == WindowResizeThumb.Right ? -windowBorderThickness : 0d;
//                    top    = 0d;
//                    bottom = contentBorderOffset;
//                    break;

//                case WindowResizeThumb.Bottom:
//                case WindowResizeThumb.Top:
//                    left   = contentBorderOffset;
//                    right  = contentBorderOffset;
//                    top    = thumbID == WindowResizeThumb.Top ? -windowBorderThickness : 0d;
//                    bottom = thumbID == WindowResizeThumb.Bottom ? -windowBorderThickness : 0d;
//                    break;

//                case WindowResizeThumb.BottomLeft:
//                case WindowResizeThumb.BottomRight:
//                    left   = thumbID == WindowResizeThumb.BottomLeft ? -windowBorderThickness : 0d;
//                    right  = thumbID == WindowResizeThumb.BottomRight ? -windowBorderThickness : 0d;
//                    top    = 0d;
//                    bottom = -windowBorderThickness;
//                    break;

//                case WindowResizeThumb.TopLeft:
//                case WindowResizeThumb.TopRight:
//                    left   = thumbID == WindowResizeThumb.TopLeft ? -windowBorderThickness : 0d;
//                    right  = thumbID == WindowResizeThumb.TopRight ? -windowBorderThickness : 0d;
//                    top    = -windowBorderThickness;
//                    bottom = 0d;
//                    break;
//            }
//        }

//        return new Thickness(left, top, right, bottom);
//    }

//    public object[] ConvertBack(object?     value,
//                                Type[]      targetTypes,
//                                object      parameter,
//                                CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}
