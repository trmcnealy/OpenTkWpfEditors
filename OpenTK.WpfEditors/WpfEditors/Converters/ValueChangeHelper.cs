﻿using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

/// <summary>
/// This helper class will raise events when a specific
/// path value on one or many items changes.
/// </summary>
internal class ValueChangeHelper : DependencyObject
{
    #region Value Property

    /// <summary>
    /// This private property serves as the target of a binding that monitors the value of the binding
    /// of each item in the source.
    /// </summary>
    private static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(ValueChangeHelper), new UIPropertyMetadata(null, OnValueChanged));

    private object Value
    {
        get
        {
            return GetValue(ValueProperty);
        }
        set
        {
            SetValue(ValueProperty, value);
        }
    }

    private static void OnValueChanged(DependencyObject                   sender,
                                       DependencyPropertyChangedEventArgs args)
    {
        ((ValueChangeHelper)sender).RaiseValueChanged();
    }

    #endregion

    public event EventHandler? ValueChanged;

    #region Constructor

    public ValueChangeHelper(Action changeCallback)
    {
        if(changeCallback == null)
        {
            throw new ArgumentNullException(nameof(changeCallback));
        }

        ValueChanged += (s,
                         args) => changeCallback();
    }

    #endregion

    #region Methods

    public void UpdateValueSource(object? sourceItem,
                                  string? path)
    {
        BindingBase? binding = null;

        if(sourceItem != null && path != null)
        {
            binding = new Binding(path)
            {
                Source = sourceItem
            };
        }

        UpdateBinding(binding);
    }

    public void UpdateValueSource(IEnumerable? sourceItems,
                                  string?      path)
    {
        BindingBase? binding = null;

        if(sourceItems != null && path != null)
        {
            MultiBinding multiBinding = new();

            multiBinding.Converter = new BlankMultiValueConverter();

            foreach(object? item in sourceItems)
            {
                multiBinding.Bindings.Add(new Binding(path)
                {
                    Source = item
                });
            }

            binding = multiBinding;
        }

        UpdateBinding(binding);
    }

    private void UpdateBinding(BindingBase? binding)
    {
        if(binding != null)
        {
            BindingOperations.SetBinding(this, ValueProperty, binding);
        }
        else
        {
            ClearBinding();
        }
    }

    private void ClearBinding()
    {
        BindingOperations.ClearBinding(this, ValueProperty);
    }

    private void RaiseValueChanged()
    {
        if(ValueChanged != null)
        {
            ValueChanged(this, EventArgs.Empty);
        }
    }

    #endregion

    #region BlankMultiValueConverter private class

    private class BlankMultiValueConverter : IMultiValueConverter
    {
        public object? Convert(object[]                         values,
                              Type                             targetType,
                              object                           parameter,
                              System.Globalization.CultureInfo culture)
        {
            // We will not use the result anyway. We just want the change notification to kick in.
            // Return a new object to have a different value.
            return new object();
        }

        public object[] ConvertBack(object?                          value,
                                    Type[]                           targetTypes,
                                    object                           parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }

    #endregion
}
