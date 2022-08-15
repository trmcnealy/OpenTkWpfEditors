using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenTK.WpfEditors.Converters;

public class ToggleSwitchSizeConverter : IMultiValueConverter
{
    public object? Convert(object[]    values,
                          Type        targetType,
                          object      parameter,
                          CultureInfo culture)
    {
        // InnerContent.Width
        if(parameter.ToString() == "InnerContent.Width")
        {
            double    width             = (double)values[0];
            double    thumbWidthPercent = (double)values[1];
            Thickness borders           = (Thickness)values[2];
            Thickness outerMargin       = (Thickness)values[3];
            Thickness innerMargin       = (Thickness)values[4];
            double    innerWidth        = (width - borders.Left - borders.Right - width * (thumbWidthPercent / 100) / 2) * 2;
            return innerWidth - outerMargin.Right - outerMargin.Left - innerMargin.Right - innerMargin.Left;
        }

        // InnerContent.Height
        if(parameter.ToString() == "InnerContent.Height")
        {
            double    height      = (double)values[0];
            Thickness borders     = (Thickness)values[1];
            Thickness outerMargin = (Thickness)values[2];
            Thickness innerMargin = (Thickness)values[3];
            return height - borders.Top - borders.Bottom - outerMargin.Top - outerMargin.Bottom - innerMargin.Top - innerMargin.Bottom;
        }

        // Thumb.Width
        if(parameter.ToString() == "Thumb.Width")
        {
            double width             = (double)values[0];
            double thumbWidthPercent = (double)values[1];
            return width * (thumbWidthPercent / 100);
        }

        // ThumbGrid.Width
        if(parameter.ToString() == "ThumbGrid.Width")
        {
            double    width             = (double)values[0];
            double    thumbWidthPercent = (double)values[1];
            Thickness borders           = (Thickness)values[2];
            double    innerWidth        = (width - borders.Left - borders.Right - width * (thumbWidthPercent / 100) / 2) * 2;
            return innerWidth + borders.Right + borders.Left;
        }

        Console.WriteLine("SizeConverter called with invalid Parameter '{0}'", parameter);

        // No other parameter is currently supported
        return 0;
    }

    public object[] ConvertBack(object?     value,
                                Type[]      targetTypes,
                                object      parameter,
                                CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
