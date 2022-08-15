using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using OpenTK.Mathematics;

namespace OpenTK.WpfEditors;

[TemplatePart(Name = PART_SpectrumDisplay, Type = typeof(Rectangle))]
public class Color4SpectrumSlider : Slider
{
    private const string PART_SpectrumDisplay = "PART_SpectrumDisplay";

    #region Private Members

    private Rectangle? _spectrumDisplay;
    private LinearGradientBrush? _pickerBrush;

    #endregion //Private Members

    #region Constructors

    static Color4SpectrumSlider()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Color4SpectrumSlider), new FrameworkPropertyMetadata(typeof(Color4SpectrumSlider)));
    }

    #endregion //Constructors

    #region Dependency Properties

    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color4), typeof(Color4SpectrumSlider), new PropertyMetadata(Colors.Transparent));

    public Color4 SelectedColor
    {
        get
        {
            return (Color4)GetValue(SelectedColorProperty);
        }
        set
        {
            SetValue(SelectedColorProperty, value);
        }
    }

    #endregion //Dependency Properties

    #region Base Class Overrides

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _spectrumDisplay = (Rectangle?)GetTemplateChild(PART_SpectrumDisplay);
        CreateSpectrum();
        OnValueChanged(double.NaN, Value);
    }

    protected override void OnValueChanged(double oldValue,
                                           double newValue)
    {
        base.OnValueChanged(oldValue, newValue);

        Color4 color = Color4Utilities.ConvertHsvToRgb(360f - (float)newValue, 1f, 1f, 1f);
        SelectedColor = color;
    }

    #endregion //Base Class Overrides

    #region Methods

    private void CreateSpectrum()
    {
        _pickerBrush = new LinearGradientBrush();
        _pickerBrush.StartPoint = new Point(0.5f, 0f);
        _pickerBrush.EndPoint = new Point(0.5f, 1f);
        _pickerBrush.ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;

        List<Color4> colorsList = Color4Utilities.GenerateHsvSpectrum();

        float stopIncrement = 1f / colorsList.Count;

        int i;
        for (i = 0; i < colorsList.Count; i++)
        {
            _pickerBrush.GradientStops.Add(new GradientStop(colorsList[i].ToSystemWindowsMedia(), i * stopIncrement));
        }

        _pickerBrush.GradientStops[i - 1].Offset = 1.0f;

        if (_spectrumDisplay != null)
        {
            _spectrumDisplay.Fill = _pickerBrush;
        }
    }

    #endregion //Methods
}
