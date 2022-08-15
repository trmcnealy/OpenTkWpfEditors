﻿//using System;
//using System.Globalization;
//using System.Windows;
//using System.Windows.Data;
//using OpenTK.WpfEditors.Controls;

//namespace OpenTK.WpfEditors.Converters;

//public class VisibilityToBoolConverter : IValueConverter
//{
//    #region Inverted Property

//    public bool Inverted
//    {
//        get
//        {
//            return _inverted;
//        }
//        set
//        {
//            _inverted = value;
//        }
//    }

//    private bool _inverted; //false

//    #endregion

//    #region Not Property

//    public bool Not
//    {
//        get
//        {
//            return _not;
//        }
//        set
//        {
//            _not = value;
//        }
//    }

//    private bool _not; //false

//    #endregion

//    public object? Convert(object?     value,
//                          Type        targetType,
//                          object      parameter,
//                          CultureInfo culture)
//    {
//        return Inverted ? BoolToVisibility(value) : VisibilityToBool(value);
//    }

//    public object? ConvertBack(object?     value,
//                              Type        targetType,
//                              object      parameter,
//                              CultureInfo culture)
//    {
//        return Inverted ? VisibilityToBool(value) : BoolToVisibility(value);
//    }

//    private object VisibilityToBool(object? value)
//    {
//        if(!(value is Visibility))
//        {
//            throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotVisibility"));
//        }

//        return ((Visibility)value == Visibility.Visible) ^ Not;
//    }

//    private object BoolToVisibility(object? value)
//    {
//        if(!(value is bool))
//        {
//            throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotBool"));
//        }

//        return (bool)value ^ Not ? Visibility.Visible : Visibility.Collapsed;
//    }
//}
