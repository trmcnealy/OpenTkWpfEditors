//using System;
//using System.Windows;
//using System.Windows.Data;
//using System.Windows.Media;
//using OpenTK.WpfEditors.Controls;

//namespace OpenTK.WpfEditors.Converters;

//public class RatingItemActiveFillOpacityMaskBrushConverter : IMultiValueConverter
//{
//    public object? Convert(object[]                         values,
//                          Type                             targetType,
//                          object                           parameter,
//                          System.Globalization.CultureInfo culture)
//    {
//        int                 iconIndex     = (int)values[0];
//        double              ratingValue   = (double)values[1];
//        int                 itemCount     = (int)values[2];
//        RatingValueType     valueType     = (RatingValueType)values[3];
//        RatingPrecision     precisionMode = (RatingPrecision)values[4];
//        RatingFillDirection fillDirection = (RatingFillDirection)values[5];
//        RatingOrientation   orientation   = (RatingOrientation)values[6];


//        // Reverse
//        if((orientation == RatingOrientation.Horizontal && fillDirection == RatingFillDirection.Left) || (orientation == RatingOrientation.Vertical && fillDirection == RatingFillDirection.Up))
//        {
//            iconIndex = itemCount - (iconIndex - 1);
//        }


//        // Step 1 : Convert an Exact value to its Percentage equivalent
//        if(valueType == RatingValueType.Exact)
//        {
//            ratingValue = ratingValue / itemCount;
//        }


//        // Step 2 : Return width percentage of the icon that is active
//        double percentage;
//        double min = iconIndex > 1 ? ((double)iconIndex - 1) / itemCount : 0;
//        double max = iconIndex / (double)itemCount;

//        // a) Full Star
//        if(ratingValue >= max)
//        {
//            percentage = 100.0;
//        }

//        // b) Empty Star
//        else if(ratingValue <= min)
//        {
//            percentage = 0.0;
//        }

//        // c) Partial Star
//        else
//        {
//            percentage = (ratingValue - min) / (max - min) * 100;

//            //Adjust percentage for Full and Half precision modes
//            if(precisionMode == RatingPrecision.Full || (precisionMode == RatingPrecision.Half && percentage > 50.0))
//            {
//                percentage = 100.0;
//            }

//            else if(precisionMode == RatingPrecision.Half && percentage < 50.0)
//            {
//                percentage = 50.0;
//            }
//        }


//        // Step 3 : Build the Brush
//        double              offset = percentage / 100;
//        LinearGradientBrush brush  = new();
//        brush.StartPoint = new Point(0, 0);

//        switch(fillDirection)
//        {
//            case RatingFillDirection.Left:
//                offset         = 1 - offset;
//                brush.EndPoint = new Point(1, 0);
//                brush.GradientStops.Add(new GradientStop(Colors.Transparent, offset));
//                brush.GradientStops.Add(new GradientStop(Colors.White,       offset));
//                break;
//            case RatingFillDirection.Right:
//                brush.EndPoint = new Point(1, 0);
//                brush.GradientStops.Add(new GradientStop(Colors.White,       offset));
//                brush.GradientStops.Add(new GradientStop(Colors.Transparent, offset));
//                break;
//            case RatingFillDirection.Up:
//                offset         = 1 - offset;
//                brush.EndPoint = new Point(0, 1);
//                brush.GradientStops.Add(new GradientStop(Colors.Transparent, offset));
//                brush.GradientStops.Add(new GradientStop(Colors.White,       offset));
//                break;
//            case RatingFillDirection.Down:
//                brush.EndPoint = new Point(0, 1);
//                brush.GradientStops.Add(new GradientStop(Colors.White,       offset));
//                brush.GradientStops.Add(new GradientStop(Colors.Transparent, offset));
//                break;
//            default:
//                break;
//        }

//        return brush;
//    }

//    public object[] ConvertBack(object?                          value,
//                                Type[]                           targetTypes,
//                                object                           parameter,
//                                System.Globalization.CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}
