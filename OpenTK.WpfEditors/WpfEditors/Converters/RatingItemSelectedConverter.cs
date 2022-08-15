//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Data;
//using OpenTK.WpfEditors;
//using OpenTK.WpfEditors.Controls;
//using ItemCollection = System.Windows.Controls.ItemCollection;

//namespace OpenTK.WpfEditors.Converters;

//public class RatingItemSelectedConverter : IMultiValueConverter
//{
//    public object? Convert(object[]                         values,
//                          Type                             targetType,
//                          object                           parameter,
//                          System.Globalization.CultureInfo culture)
//    {
//        bool                bReverse      = false;
//        int                 iconIndex     = (int)values[0];
//        int                 itemCount     = (int)values[1];
//        RatingFillDirection fillDirection = (RatingFillDirection)values[2];
//        RatingOrientation   orientation   = (RatingOrientation)values[3];

//        // Reverse
//        int value = iconIndex;
//        if((orientation == RatingOrientation.Horizontal && fillDirection == RatingFillDirection.Left) || (orientation == RatingOrientation.Vertical && fillDirection == RatingFillDirection.Up))
//        {
//            bReverse = true;
//            value    = itemCount - (iconIndex - 1);
//        }


//        if(parameter == null)
//        {
//            double          ratingValue = (double)values[4];
//            RatingValueType valueType   = (RatingValueType)values[5];


//            // Step 1 : Convert an Exact value to its Percentage equivalent
//            if(valueType == RatingValueType.Exact)
//            {
//                ratingValue = ratingValue / itemCount;
//            }


//            // Step 2 : Return if this icon should display as active or not
//            double min = value > 1 ? ((double)value - 1) / itemCount : 0;

//            // a) Empty Star
//            if(ratingValue <= min)
//            {
//                return false;
//            }

//            // b) Full or Partial Star
//            else
//            {
//                return true;
//            }
//        }

//        // Hover State
//        if(parameter.ToString().ToUpper() == "HOVER")
//        {
//            bool           isMouseOver = (bool)values[4];
//            ItemCollection items       = (ItemCollection)values[5];

//            if(isMouseOver)
//            {
//                for(int i = 0; i < value - 1; i++)
//                {
//                    RatingItem item;
//                    if(!bReverse)
//                    {
//                        item = items[i] as RatingItem;
//                    }
//                    else
//                    {
//                        item = items[itemCount - 1 - i] as RatingItem;
//                    }

//                    if(item.IsMouseOver)
//                    {
//                        return false;
//                    }
//                }

//                return true;
//            }
//        }

//        // No other parameter is supported at this time
//        return false;
//    }

//    public object[] ConvertBack(object?                          value,
//                                Type[]                           targetTypes,
//                                object                           parameter,
//                                System.Globalization.CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}
