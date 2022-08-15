using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using OpenTK.Mathematics;

namespace OpenTK.WpfEditors;

[TemplatePart(Name = PART_ColorShadingCanvas, Type = typeof(Canvas))]
[TemplatePart(Name = PART_ColorShadeSelector, Type = typeof(Canvas))]
[TemplatePart(Name = PART_SpectrumSlider, Type = typeof(Color4SpectrumSlider))]
[TemplatePart(Name = PART_HexadecimalTextBox, Type = typeof(TextBox))]
public class Color4Canvas : Control
{
    private const string PART_ColorShadingCanvas = "PART_ColorShadingCanvas";
    private const string PART_ColorShadeSelector = "PART_ColorShadeSelector";
    private const string PART_SpectrumSlider = "PART_SpectrumSlider";
    private const string PART_HexadecimalTextBox = "PART_HexadecimalTextBox";

    #region Private Members

    private TranslateTransform _colorShadeSelectorTransform = new();
    private Canvas? _colorShadingCanvas;
    private Canvas? _colorShadeSelector;
    private Color4SpectrumSlider? _spectrumSlider;
    private TextBox? _hexadecimalTextBox;
    private Point? _currentColorPosition;
    private bool _surpressPropertyChanged;

    #endregion //Private Members

    #region Properties

    #region SelectedColor

    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor",
                                                                                                  typeof(Color4),
                                                                                                  typeof(Color4Canvas),
                                                                                                  new FrameworkPropertyMetadata(Color4.Black,
                                                                                                                                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                                                                                OnSelectedColorChanged));

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

    private static void OnSelectedColorChanged(DependencyObject o,
                                               DependencyPropertyChangedEventArgs e)
    {
        if (o is Color4Canvas colorCanvas)
        {
            colorCanvas.OnSelectedColorChanged((Color4)e.OldValue, (Color4)e.NewValue);
        }
    }

    protected virtual void OnSelectedColorChanged(Color4 oldValue,
                                                  Color4 newValue)
    {
        SetHexadecimalStringProperty(GetFormatedColorString(newValue), false);
        UpdateRGBValues(newValue);
        UpdateColorShadeSelectorPosition(newValue);

        RoutedPropertyChangedEventArgs<Color4> args = new(oldValue, newValue)
        {
            RoutedEvent = SelectedColorChangedEvent
        };
        RaiseEvent(args);
    }

    #endregion //SelectedColor

    #region RGB

    #region A

    public static readonly DependencyProperty AProperty = DependencyProperty.Register("A", typeof(float), typeof(Color4Canvas), new UIPropertyMetadata(1.0f, OnAChanged));

    public float A
    {
        get
        {
            return (float)GetValue(AProperty);
        }
        set
        {
            SetValue(AProperty, value);
        }
    }

    private static void OnAChanged(DependencyObject o,
                                   DependencyPropertyChangedEventArgs e)
    {
        if (o is Color4Canvas colorCanvas)
        {
            colorCanvas.OnAChanged((float)e.OldValue, (float)e.NewValue);
        }
    }

    protected virtual void OnAChanged(float oldValue,
                                      float newValue)
    {
        if (!_surpressPropertyChanged)
        {
            UpdateSelectedColor();
        }
    }

    #endregion //A

    #region R

    public static readonly DependencyProperty RProperty = DependencyProperty.Register("R", typeof(float), typeof(Color4Canvas), new UIPropertyMetadata(0f, OnRChanged));

    public float R
    {
        get
        {
            return (float)GetValue(RProperty);
        }
        set
        {
            SetValue(RProperty, value);
        }
    }

    private static void OnRChanged(DependencyObject o,
                                   DependencyPropertyChangedEventArgs e)
    {
        if (o is Color4Canvas colorCanvas)
        {
            colorCanvas.OnRChanged((float)e.OldValue, (float)e.NewValue);
        }
    }

    protected virtual void OnRChanged(float oldValue,
                                      float newValue)
    {
        if (!_surpressPropertyChanged)
        {
            UpdateSelectedColor();
        }
    }

    #endregion //R

    #region G

    public static readonly DependencyProperty GProperty = DependencyProperty.Register("G", typeof(float), typeof(Color4Canvas), new UIPropertyMetadata(0f, OnGChanged));

    public float G
    {
        get
        {
            return (float)GetValue(GProperty);
        }
        set
        {
            SetValue(GProperty, value);
        }
    }

    private static void OnGChanged(DependencyObject o,
                                   DependencyPropertyChangedEventArgs e)
    {
        if (o is Color4Canvas colorCanvas)
        {
            colorCanvas.OnGChanged((float)e.OldValue, (float)e.NewValue);
        }
    }

    protected virtual void OnGChanged(float oldValue,
                                      float newValue)
    {
        if (!_surpressPropertyChanged)
        {
            UpdateSelectedColor();
        }
    }

    #endregion //G

    #region B

    public static readonly DependencyProperty BProperty = DependencyProperty.Register("B", typeof(float), typeof(Color4Canvas), new UIPropertyMetadata(0f, OnBChanged));

    public float B
    {
        get
        {
            return (float)GetValue(BProperty);
        }
        set
        {
            SetValue(BProperty, value);
        }
    }

    private static void OnBChanged(DependencyObject o,
                                   DependencyPropertyChangedEventArgs e)
    {
        if (o is Color4Canvas colorCanvas)
        {
            colorCanvas.OnBChanged((float)e.OldValue, (float)e.NewValue);
        }
    }

    protected virtual void OnBChanged(float oldValue,
                                      float newValue)
    {
        if (!_surpressPropertyChanged)
        {
            UpdateSelectedColor();
        }
    }

    #endregion //B

    #endregion //RGB

    #region HexadecimalString

    public static readonly DependencyProperty HexadecimalStringProperty = DependencyProperty.Register("HexadecimalString",
                                                                                                      typeof(string),
                                                                                                      typeof(Color4Canvas),
                                                                                                      new UIPropertyMetadata("#FFFFFFFF",
                                                                                                                             OnHexadecimalStringChanged,
                                                                                                                             OnCoerceHexadecimalString));

    public string HexadecimalString
    {
        get
        {
            return (string)GetValue(HexadecimalStringProperty);
        }
        set
        {
            SetValue(HexadecimalStringProperty, value);
        }
    }

    private static void OnHexadecimalStringChanged(DependencyObject o,
                                                   DependencyPropertyChangedEventArgs e)
    {
        if (o is Color4Canvas colorCanvas)
        {
            colorCanvas.OnHexadecimalStringChanged((string)e.OldValue, (string)e.NewValue);
        }
    }

    protected virtual void OnHexadecimalStringChanged(string oldValue,
                                                      string newValue)
    {
        string newColorString = GetFormatedColorString(newValue);
        string currentColorString = GetFormatedColorString(SelectedColor);

        if (!currentColorString.Equals(newColorString))
        {
            UpdateSelectedColor((Color4)ColorConverter.ConvertFromString(newColorString));
        }

        SetHexadecimalTextBoxTextProperty(newValue);
    }

    private static object? OnCoerceHexadecimalString(DependencyObject d,
                                                     object? basevalue)
    {
        Color4Canvas? colorCanvas = (Color4Canvas?)d;

        if (colorCanvas == null)
        {
            return basevalue;
        }

        return colorCanvas.OnCoerceHexadecimalString(basevalue);
    }

    private object? OnCoerceHexadecimalString(object? newValue)
    {
        string? value = newValue as string;
        string? retValue = value;

        try
        {
            ColorConverter.ConvertFromString(value);
        }
        catch
        {
            //When HexadecimalString is changed via Code-Behind and hexadecimal format is bad, throw.
            throw new InvalidDataException("Color4 provided is not in the correct format.");
        }

        return retValue;
    }

    #endregion //HexadecimalString

    #region UsingAlphaChannel

    public static readonly DependencyProperty UsingAlphaChannelProperty = DependencyProperty.Register("UsingAlphaChannel",
                                                                                                      typeof(bool),
                                                                                                      typeof(Color4Canvas),
                                                                                                      new FrameworkPropertyMetadata(true,
                                                                                                                                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                                                                                    OnUsingAlphaChannelPropertyChanged));

    public bool UsingAlphaChannel
    {
        get
        {
            return (bool)GetValue(UsingAlphaChannelProperty);
        }
        set
        {
            SetValue(UsingAlphaChannelProperty, value);
        }
    }

    private static void OnUsingAlphaChannelPropertyChanged(DependencyObject o,
                                                           DependencyPropertyChangedEventArgs e)
    {
        if (o is Color4Canvas colorCanvas)
        {
            colorCanvas.OnUsingAlphaChannelChanged();
        }
    }

    protected virtual void OnUsingAlphaChannelChanged()
    {
        SetHexadecimalStringProperty(GetFormatedColorString(SelectedColor), false);
    }

    #endregion //UsingAlphaChannel

    #endregion //Properties

    #region Constructors

    static Color4Canvas()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Color4Canvas), new FrameworkPropertyMetadata(typeof(Color4Canvas)));
    }

    #endregion //Constructors

    #region Base Class Overrides

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_colorShadingCanvas != null)
        {
            _colorShadingCanvas.MouseLeftButtonDown -= ColorShadingCanvas_MouseLeftButtonDown;
            _colorShadingCanvas.MouseLeftButtonUp -= ColorShadingCanvas_MouseLeftButtonUp;
            _colorShadingCanvas.MouseMove -= ColorShadingCanvas_MouseMove;
            _colorShadingCanvas.SizeChanged -= ColorShadingCanvas_SizeChanged;
        }

        _colorShadingCanvas = GetTemplateChild(PART_ColorShadingCanvas) as Canvas;

        if (_colorShadingCanvas != null)
        {
            _colorShadingCanvas.MouseLeftButtonDown += ColorShadingCanvas_MouseLeftButtonDown;
            _colorShadingCanvas.MouseLeftButtonUp += ColorShadingCanvas_MouseLeftButtonUp;
            _colorShadingCanvas.MouseMove += ColorShadingCanvas_MouseMove;
            _colorShadingCanvas.SizeChanged += ColorShadingCanvas_SizeChanged;
        }

        _colorShadeSelector = GetTemplateChild(PART_ColorShadeSelector) as Canvas;

        if (_colorShadeSelector != null)
        {
            _colorShadeSelector.RenderTransform = _colorShadeSelectorTransform;
        }

        if (_spectrumSlider != null)
        {
            _spectrumSlider.ValueChanged -= SpectrumSlider_ValueChanged;
        }

        _spectrumSlider = GetTemplateChild(PART_SpectrumSlider) as Color4SpectrumSlider;

        if (_spectrumSlider != null)
        {
            _spectrumSlider.ValueChanged += SpectrumSlider_ValueChanged;
        }

        if (_hexadecimalTextBox != null)
        {
            _hexadecimalTextBox.LostFocus -= HexadecimalTextBox_LostFocus;
        }

        _hexadecimalTextBox = GetTemplateChild(PART_HexadecimalTextBox) as TextBox;

        if (_hexadecimalTextBox != null)
        {
            _hexadecimalTextBox.LostFocus += HexadecimalTextBox_LostFocus;
        }

        UpdateRGBValues(SelectedColor);
        UpdateColorShadeSelectorPosition(SelectedColor);

        // When changing theme, HexadecimalString needs to be set since it is not binded.
        SetHexadecimalTextBoxTextProperty(GetFormatedColorString(SelectedColor));
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        //hitting enter on textbox will update Hexadecimal string
        if (e.Key == Key.Enter && e.OriginalSource is TextBox)
        {
            TextBox textBox = (TextBox)e.OriginalSource;
            if (textBox.Name == PART_HexadecimalTextBox)
            {
                SetHexadecimalStringProperty(textBox.Text, true);
            }
        }
    }

    #endregion //Base Class Overrides

    #region Event Handlers

    private void ColorShadingCanvas_MouseLeftButtonDown(object? sender,
                                                        MouseButtonEventArgs e)
    {
        Point p = e.GetPosition(_colorShadingCanvas);
        UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
        _colorShadingCanvas?.CaptureMouse();
    }

    private void ColorShadingCanvas_MouseLeftButtonUp(object? sender,
                                                      MouseButtonEventArgs e)
    {
        _colorShadingCanvas?.ReleaseMouseCapture();
    }

    private void ColorShadingCanvas_MouseMove(object? sender,
                                              MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            Point p = e.GetPosition(_colorShadingCanvas);
            UpdateColorShadeSelectorPositionAndCalculateColor(p, true);
            Mouse.Synchronize();
        }
    }

    private void ColorShadingCanvas_SizeChanged(object? sender,
                                                SizeChangedEventArgs e)
    {
        if (_currentColorPosition != null)
        {
            Point _newPoint = new()
            {
                X = ((Point)_currentColorPosition).X * e.NewSize.Width,
                Y = ((Point)_currentColorPosition).Y * e.NewSize.Height
            };

            UpdateColorShadeSelectorPositionAndCalculateColor(_newPoint, false);
        }
    }

    private void SpectrumSlider_ValueChanged(object? sender,
                                             RoutedPropertyChangedEventArgs<double> e)
    {
        if (_currentColorPosition != null)
        {
            CalculateColor((Point)_currentColorPosition);
        }
    }

    private void HexadecimalTextBox_LostFocus(object? sender,
                                              RoutedEventArgs e)
    {
        if(sender is TextBox textbox)
        {
            SetHexadecimalStringProperty(textbox.Text, true);
        }
    }

    #endregion //Event Handlers

    #region Events

    public static readonly RoutedEvent SelectedColorChangedEvent =
        EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color4>), typeof(Color4Canvas));

    public event RoutedPropertyChangedEventHandler<Color4> SelectedColorChanged
    {
        add
        {
            AddHandler(SelectedColorChangedEvent, value);
        }
        remove
        {
            RemoveHandler(SelectedColorChangedEvent, value);
        }
    }

    #endregion //Events

    #region Methods

    private void UpdateSelectedColor()
    {
        SelectedColor = new Color4(R, G, B, A);
    }

    private void UpdateSelectedColor(Color4 color)
    {
        SelectedColor = new Color4(color.R, color.G, color.B, color.A);
    }

    private void UpdateRGBValues(Color4 color)
    {
        _surpressPropertyChanged = true;

        A = color.A;
        R = color.R;
        G = color.G;
        B = color.B;

        _surpressPropertyChanged = false;
    }

    private void UpdateColorShadeSelectorPositionAndCalculateColor(Point p,
                                                                   bool calculateColor)
    {
        if (p.Y < 0)
        {
            p.Y = 0;
        }

        if (p.X < 0)
        {
            p.X = 0;
        }

        if (_colorShadingCanvas != null && _colorShadeSelector != null)
        {
            if (p.X > _colorShadingCanvas.ActualWidth)
            {
                p.X = _colorShadingCanvas.ActualWidth;
            }

            if (p.Y > _colorShadingCanvas.ActualHeight)
            {
                p.Y = _colorShadingCanvas.ActualHeight;
            }

            _colorShadeSelectorTransform.X = p.X - _colorShadeSelector.Width / 2f;
            _colorShadeSelectorTransform.Y = p.Y - _colorShadeSelector.Height / 2f;

            p.X = p.X / _colorShadingCanvas.ActualWidth;
            p.Y = p.Y / _colorShadingCanvas.ActualHeight;
        }

        _currentColorPosition = p;

        if (calculateColor)
        {
            CalculateColor(p);
        }
    }

    private void UpdateColorShadeSelectorPosition(Color4 color)
    {
        if (_spectrumSlider == null || _colorShadingCanvas == null)
        {
            return;
        }

        _currentColorPosition = null;

        HsvColor4 hsv = Color4Utilities.ConvertRgbToHsv(color.R, color.G, color.B, color.A);

        if (!(Math.Abs(color.R - color.G) < float.Epsilon && Math.Abs(color.R - color.B) < float.Epsilon))
        {
            _spectrumSlider.Value = hsv.H;
        }

        Point p = new(hsv.S, 1 - hsv.V);

        _currentColorPosition = p;

        _colorShadeSelectorTransform.X = p.X * _colorShadingCanvas.Width - 5;
        _colorShadeSelectorTransform.Y = p.Y * _colorShadingCanvas.Height - 5;
    }

    private void CalculateColor(Point p)
    {
        if (_spectrumSlider != null)
        {
            HsvColor4 hsv = new(360f - (float)_spectrumSlider.Value, 1f, 1f, 1f)
            {
                S = (float)p.X,
                V = 1f - (float)p.Y
            };
            Color4 currentColor = Color4Utilities.ConvertHsvToRgb(hsv.H, hsv.S, hsv.V, hsv.A);
            currentColor.A = A;
            SelectedColor = currentColor;
        }

        SetHexadecimalStringProperty(GetFormatedColorString(SelectedColor), false);
    }

    private string GetFormatedColorString(Color4 colorToFormat)
    {
        return Color4Utilities.FormatColorString(colorToFormat.ToString(), UsingAlphaChannel);
    }

    private string GetFormatedColorString(string stringToFormat)
    {
        return Color4Utilities.FormatColorString(stringToFormat, UsingAlphaChannel);
    }

    private void SetHexadecimalStringProperty(string newValue,
                                              bool modifyFromUI)
    {
        if (modifyFromUI)
        {
            try
            {
                ColorConverter.ConvertFromString(newValue);
                HexadecimalString = newValue;
            }
            catch
            {
                //When HexadecimalString is changed via UI and hexadecimal format is bad, keep the previous HexadecimalString.
                SetHexadecimalTextBoxTextProperty(HexadecimalString);
            }
        }
        else
        {
            //When HexadecimalString is changed via Code-Behind, hexadecimal format will be evaluated in OnCoerceHexadecimalString()
            HexadecimalString = newValue;
        }
    }

    private void SetHexadecimalTextBoxTextProperty(string newValue)
    {
        if (_hexadecimalTextBox != null)
        {
            _hexadecimalTextBox.Text = newValue;
        }
    }

    #endregion //Methods
}
