// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using OpenTK.Mathematics;

namespace OpenTK.WpfEditors;

public enum ColorMode
{
    ColorPalette,
    ColorCanvas
}

public enum ColorSortingMode
{
    Alphabetical,
    HueSaturationBrightness
}

[TemplatePart(Name = PART_AvailableColors, Type = typeof(ListBox))]
[TemplatePart(Name = PART_StandardColors, Type = typeof(ListBox))]
[TemplatePart(Name = PART_RecentColors, Type = typeof(ListBox))]
[TemplatePart(Name = PART_ColorPickerToggleButton, Type = typeof(ToggleButton))]
[TemplatePart(Name = PART_ColorPickerPalettePopup, Type = typeof(Popup))]
[TemplatePart(Name = PART_ColorModeButton, Type = typeof(Button))]
public class Color4Picker : Control
{
    private const string PART_AvailableColors = "PART_AvailableColors";
    private const string PART_StandardColors = "PART_StandardColors";
    private const string PART_RecentColors = "PART_RecentColors";
    private const string PART_ColorPickerToggleButton = "PART_ColorPickerToggleButton";
    private const string PART_ColorPickerPalettePopup = "PART_ColorPickerPalettePopup";
    private const string PART_ColorModeButton = "PART_ColorModeButton";

    #region Members

    private ListBox?      _availableColors;
    private ListBox?      _standardColors;
    private ListBox?      _recentColors;
    private ToggleButton? _toggleButton;
    private Popup?        _popup;
    private Button?       _colorModeButton;
    private bool          _selectionChanged;

    #endregion //Members

    #region Properties

    #region AvailableColors

    public static readonly DependencyProperty AvailableColorsProperty =
        DependencyProperty.Register("AvailableColors", typeof(ObservableCollection<Color4Item>), typeof(Color4Picker), new UIPropertyMetadata(CreateAvailableColors()));

    public ObservableCollection<Color4Item> AvailableColors
    {
        get
        {
            return (ObservableCollection<Color4Item>)GetValue(AvailableColorsProperty);
        }
        set
        {
            SetValue(AvailableColorsProperty, value);
        }
    }

    #endregion //AvailableColors

    #region AvailableColorsSortingMode

    public static readonly DependencyProperty AvailableColorsSortingModeProperty = DependencyProperty.Register("AvailableColorsSortingMode",
                                                                                                               typeof(ColorSortingMode),
                                                                                                               typeof(Color4Picker),
                                                                                                               new UIPropertyMetadata(ColorSortingMode.Alphabetical, OnAvailableColorsSortingModeChanged));

    public ColorSortingMode AvailableColorsSortingMode
    {
        get
        {
            return (ColorSortingMode)GetValue(AvailableColorsSortingModeProperty);
        }
        set
        {
            SetValue(AvailableColorsSortingModeProperty, value);
        }
    }

    private static void OnAvailableColorsSortingModeChanged(DependencyObject d,
                                                            DependencyPropertyChangedEventArgs e)
    {
        if (d is Color4Picker colorPicker)
        {
            colorPicker.OnAvailableColorsSortingModeChanged((ColorSortingMode)e.OldValue, (ColorSortingMode)e.NewValue);
        }
    }

    private void OnAvailableColorsSortingModeChanged(ColorSortingMode oldValue,
                                                     ColorSortingMode newValue)
    {
        if (CollectionViewSource.GetDefaultView(AvailableColors) is ListCollectionView lcv)
        {
            lcv.CustomSort = AvailableColorsSortingMode == ColorSortingMode.HueSaturationBrightness ? new ColorSorter() : null;
        }
    }

    #endregion //AvailableColorsSortingMode

    #region AvailableColorsHeader

    public static readonly DependencyProperty AvailableColorsHeaderProperty =
        DependencyProperty.Register("AvailableColorsHeader", typeof(string), typeof(Color4Picker), new UIPropertyMetadata("Available Colors"));

    public string AvailableColorsHeader
    {
        get
        {
            return (string)GetValue(AvailableColorsHeaderProperty);
        }
        set
        {
            SetValue(AvailableColorsHeaderProperty, value);
        }
    }

    #endregion //AvailableColorsHeader

    #region ButtonStyle

    public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(Color4Picker));

    public Style ButtonStyle
    {
        get
        {
            return (Style)GetValue(ButtonStyleProperty);
        }
        set
        {
            SetValue(ButtonStyleProperty, value);
        }
    }

    #endregion //ButtonStyle

    #region DisplayColorAndName

    public static readonly DependencyProperty DisplayColorAndNameProperty = DependencyProperty.Register("DisplayColorAndName", typeof(bool), typeof(Color4Picker), new UIPropertyMetadata(false));

    public bool DisplayColorAndName
    {
        get
        {
            return (bool)GetValue(DisplayColorAndNameProperty);
        }
        set
        {
            SetValue(DisplayColorAndNameProperty, value);
        }
    }

    #endregion //DisplayColorAndName

    #region ColorMode

    public static readonly DependencyProperty ColorModeProperty = DependencyProperty.Register("ColorMode", typeof(ColorMode), typeof(Color4Picker), new UIPropertyMetadata(ColorMode.ColorPalette));

    public ColorMode ColorMode
    {
        get
        {
            return (ColorMode)GetValue(ColorModeProperty);
        }
        set
        {
            SetValue(ColorModeProperty, value);
        }
    }

    #endregion //ColorMode

    #region IsOpen

    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(Color4Picker), new UIPropertyMetadata(false));

    public bool IsOpen
    {
        get
        {
            return (bool)GetValue(IsOpenProperty);
        }
        set
        {
            SetValue(IsOpenProperty, value);
        }
    }

    #endregion //IsOpen

    #region RecentColors

    public static readonly DependencyProperty RecentColorsProperty = DependencyProperty.Register("RecentColors", typeof(ObservableCollection<Color4Item>), typeof(Color4Picker), new UIPropertyMetadata(null));

    public ObservableCollection<Color4Item> RecentColors
    {
        get
        {
            return (ObservableCollection<Color4Item>)GetValue(RecentColorsProperty);
        }
        set
        {
            SetValue(RecentColorsProperty, value);
        }
    }

    #endregion //RecentColors

    #region RecentColorsHeader

    public static readonly DependencyProperty RecentColorsHeaderProperty = DependencyProperty.Register("RecentColorsHeader", typeof(string), typeof(Color4Picker), new UIPropertyMetadata("Recent Colors"));

    public string RecentColorsHeader
    {
        get
        {
            return (string)GetValue(RecentColorsHeaderProperty);
        }
        set
        {
            SetValue(RecentColorsHeaderProperty, value);
        }
    }

    #endregion //RecentColorsHeader

    #region SelectedColor

    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor",
                                                                                                  typeof(Color4),
                                                                                                  typeof(Color4Picker),
                                                                                                  new FrameworkPropertyMetadata(Colors.Black,
                                                                                                                                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                                                                                OnSelectedColorPropertyChanged));

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

    private static void OnSelectedColorPropertyChanged(DependencyObject d,
                                                       DependencyPropertyChangedEventArgs e)
    {
        if (d is Color4Picker colorPicker)
        {
            colorPicker.OnSelectedColorChanged((Color)e.OldValue, (Color)e.NewValue);
        }
    }

    private void OnSelectedColorChanged(Color oldValue,
                                        Color newValue)
    {
        SelectedColorText = GetFormatedColorString(newValue.ToOpenTK());

        RoutedPropertyChangedEventArgs<Color> args = new(oldValue, newValue);
        args.RoutedEvent = SelectedColorChangedEvent;
        RaiseEvent(args);
    }

    #endregion //SelectedColor

    #region SelectedColorText

    public static readonly DependencyProperty SelectedColorTextProperty = DependencyProperty.Register("SelectedColorText", typeof(string), typeof(Color4Picker), new UIPropertyMetadata("Black"));

    public string SelectedColorText
    {
        get
        {
            return (string)GetValue(SelectedColorTextProperty);
        }
        protected set
        {
            SetValue(SelectedColorTextProperty, value);
        }
    }

    #endregion //SelectedColorText

    #region ShowAdvancedButton

    public static readonly DependencyProperty ShowAdvancedButtonProperty = DependencyProperty.Register("ShowAdvancedButton", typeof(bool), typeof(Color4Picker), new UIPropertyMetadata(true));

    public bool ShowAdvancedButton
    {
        get
        {
            return (bool)GetValue(ShowAdvancedButtonProperty);
        }
        set
        {
            SetValue(ShowAdvancedButtonProperty, value);
        }
    }

    #endregion //ShowAdvancedButton

    #region ShowAvailableColors

    public static readonly DependencyProperty ShowAvailableColorsProperty = DependencyProperty.Register("ShowAvailableColors", typeof(bool), typeof(Color4Picker), new UIPropertyMetadata(true));

    public bool ShowAvailableColors
    {
        get
        {
            return (bool)GetValue(ShowAvailableColorsProperty);
        }
        set
        {
            SetValue(ShowAvailableColorsProperty, value);
        }
    }

    #endregion //ShowAvailableColors

    #region ShowRecentColors

    public static readonly DependencyProperty ShowRecentColorsProperty = DependencyProperty.Register("ShowRecentColors", typeof(bool), typeof(Color4Picker), new UIPropertyMetadata(false));

    public bool ShowRecentColors
    {
        get
        {
            return (bool)GetValue(ShowRecentColorsProperty);
        }
        set
        {
            SetValue(ShowRecentColorsProperty, value);
        }
    }

    #endregion //DisplayRecentColors

    #region ShowStandardColors

    public static readonly DependencyProperty ShowStandardColorsProperty = DependencyProperty.Register("ShowStandardColors", typeof(bool), typeof(Color4Picker), new UIPropertyMetadata(true));

    public bool ShowStandardColors
    {
        get
        {
            return (bool)GetValue(ShowStandardColorsProperty);
        }
        set
        {
            SetValue(ShowStandardColorsProperty, value);
        }
    }

    #endregion //DisplayStandardColors

    #region ShowDropDownButton

    public static readonly DependencyProperty ShowDropDownButtonProperty = DependencyProperty.Register("ShowDropDownButton", typeof(bool), typeof(Color4Picker), new UIPropertyMetadata(true));

    public bool ShowDropDownButton
    {
        get
        {
            return (bool)GetValue(ShowDropDownButtonProperty);
        }
        set
        {
            SetValue(ShowDropDownButtonProperty, value);
        }
    }

    #endregion //ShowDropDownButton

    #region StandardColors

    public static readonly DependencyProperty StandardColorsProperty =
        DependencyProperty.Register("StandardColors", typeof(ObservableCollection<Color4Item>), typeof(Color4Picker), new UIPropertyMetadata(CreateStandardColors()));

    public ObservableCollection<Color4Item> StandardColors
    {
        get
        {
            return (ObservableCollection<Color4Item>)GetValue(StandardColorsProperty);
        }
        set
        {
            SetValue(StandardColorsProperty, value);
        }
    }

    #endregion //StandardColors

    #region StandardColorsHeader

    public static readonly DependencyProperty StandardColorsHeaderProperty = DependencyProperty.Register("StandardColorsHeader", typeof(string), typeof(Color4Picker), new UIPropertyMetadata("Standard Colors"));

    public string StandardColorsHeader
    {
        get
        {
            return (string)GetValue(StandardColorsHeaderProperty);
        }
        set
        {
            SetValue(StandardColorsHeaderProperty, value);
        }
    }

    #endregion //StandardColorsHeader

    #region UsingAlphaChannel

    public static readonly DependencyProperty UsingAlphaChannelProperty = DependencyProperty.Register("UsingAlphaChannel",
                                                                                                      typeof(bool),
                                                                                                      typeof(Color4Picker),
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

    private static void OnUsingAlphaChannelPropertyChanged(DependencyObject d,
                                                           DependencyPropertyChangedEventArgs e)
    {
        if (d is Color4Picker colorPicker)
        {
            colorPicker.OnUsingAlphaChannelChanged();
        }
    }

    private void OnUsingAlphaChannelChanged()
    {
        SelectedColorText = GetFormatedColorString(SelectedColor);
    }

    #endregion //UsingAlphaChannel

    #endregion //Properties

    #region Constructors

    static Color4Picker()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Color4Picker), new FrameworkPropertyMetadata(typeof(Color4Picker)));
    }

    public Color4Picker()
    {
        RecentColors = new ObservableCollection<Color4Item>();
        Keyboard.AddKeyDownHandler(this, OnKeyDown);
        Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
    }

    #endregion //Constructors

    #region Base Class Overrides

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_availableColors != null)
        {
            _availableColors.SelectionChanged -= Color_SelectionChanged;
        }

        _availableColors = GetTemplateChild(PART_AvailableColors) as ListBox;
        if (_availableColors != null)
        {
            _availableColors.SelectionChanged += Color_SelectionChanged;
        }

        if (_standardColors != null)
        {
            _standardColors.SelectionChanged -= Color_SelectionChanged;
        }

        _standardColors = GetTemplateChild(PART_StandardColors) as ListBox;
        if (_standardColors != null)
        {
            _standardColors.SelectionChanged += Color_SelectionChanged;
        }

        if (_recentColors != null)
        {
            _recentColors.SelectionChanged -= Color_SelectionChanged;
        }

        _recentColors = GetTemplateChild(PART_RecentColors) as ListBox;
        if (_recentColors != null)
        {
            _recentColors.SelectionChanged += Color_SelectionChanged;
        }

        if (_popup != null)
        {
            _popup.Opened -= Popup_Opened;
        }

        _popup = GetTemplateChild(PART_ColorPickerPalettePopup) as Popup;
        if (_popup != null)
        {
            _popup.Opened += Popup_Opened;
        }

        _toggleButton = Template.FindName(PART_ColorPickerToggleButton, this) as ToggleButton;

        if (_colorModeButton != null)
        {
            _colorModeButton.Click -= ColorModeButton_Clicked;
        }

        _colorModeButton = Template.FindName(PART_ColorModeButton, this) as Button;

        if (_colorModeButton != null)
        {
            _colorModeButton.Click += ColorModeButton_Clicked;
        }
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        base.OnMouseUp(e);

        // Close Color4Picker on MouseUp to prevent action of mouseUp on controls behind the Color4Picker.
        if (_selectionChanged)
        {
            CloseColorPicker(true);
            _selectionChanged = false;
        }
    }

    #endregion //Base Class Overrides

    #region Event Handlers

    private void OnKeyDown(object? sender,
                           KeyEventArgs e)
    {
        if (!IsOpen)
        {
            if (KeyboardUtilities.IsKeyModifyingPopupState(e))
            {
                IsOpen = true;
                // Focus will be on ListBoxItem in Popup_Opened().
                e.Handled = true;
            }
        }
        else
        {
            if (KeyboardUtilities.IsKeyModifyingPopupState(e))
            {
                CloseColorPicker(true);
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                CloseColorPicker(true);
                e.Handled = true;
            }
        }
    }

    private void OnMouseDownOutsideCapturedElement(object? sender,
                                                   MouseButtonEventArgs e)
    {
        CloseColorPicker(false);
    }

    private void Color_SelectionChanged(object? sender,
                                        SelectionChangedEventArgs e)
    {
        ListBox? lb = sender as ListBox;

        if (e.AddedItems.Count > 0)
        {
            if(e.AddedItems[0] is Color4Item colorItem)
            {
                SelectedColor = colorItem.Color;
                UpdateRecentColors(colorItem);
            }

            _selectionChanged = true;
            if(lb != null)
            {
                lb.SelectedIndex = -1; //for now I don't care about keeping track of the selected color
            }
        }
    }

    private void Popup_Opened(object? sender,
                              EventArgs e)
    {
        if (_availableColors != null && ShowAvailableColors)
        {
            FocusOnListBoxItem(_availableColors);
        }
        else if (_standardColors != null && ShowStandardColors)
        {
            FocusOnListBoxItem(_standardColors);
        }
        else if (_recentColors != null && ShowRecentColors)
        {
            FocusOnListBoxItem(_recentColors);
        }
    }

    private void FocusOnListBoxItem(ListBox? listBox)
    {
        if(listBox != null)
        {
            ListBoxItem? listBoxItem = listBox.ItemContainerGenerator.ContainerFromItem(listBox.SelectedItem) as ListBoxItem;

            if (listBoxItem == null && listBox.Items.Count > 0)
            {
                listBoxItem = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromItem(listBox.Items[0]);
            }

            if (listBoxItem != null)
            {
                listBoxItem.Focus();
            }
        }
    }

    private void ColorModeButton_Clicked(object? sender,
                                         RoutedEventArgs e)
    {
        ColorMode = ColorMode == ColorMode.ColorPalette ? ColorMode.ColorCanvas : ColorMode.ColorPalette;
    }

    #endregion //Event Handlers

    #region Events

    public static readonly RoutedEvent SelectedColorChangedEvent =
        EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(Color4Picker));

    public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
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

    private void CloseColorPicker(bool isFocusOnColorPicker)
    {
        if (IsOpen)
        {
            IsOpen = false;
        }

        ReleaseMouseCapture();

        if (isFocusOnColorPicker && _toggleButton != null)
        {
            _toggleButton.Focus();
        }

        UpdateRecentColors(new Color4Item(SelectedColor, SelectedColorText));
    }

    private void UpdateRecentColors(Color4Item colorItem)
    {
        if (!RecentColors.Contains(colorItem))
        {
            RecentColors.Add(colorItem);
        }

        if (RecentColors.Count > 10) //don't allow more than ten, maybe make a property that can be set by the user.
        {
            RecentColors.RemoveAt(0);
        }
    }

    private string GetFormatedColorString(Color4 colorToFormat)
    {
        return Color4Utilities.FormatColorString(colorToFormat.GetColorName(), UsingAlphaChannel);
    }

    private static ObservableCollection<Color4Item> CreateStandardColors()
    {
        ObservableCollection<Color4Item> _standardColors = new();
        _standardColors.Add(new Color4Item(Color4.Transparent, "Transparent"));
        _standardColors.Add(new Color4Item(Color4.White, "White"));
        _standardColors.Add(new Color4Item(Color4.Gray, "Gray"));
        _standardColors.Add(new Color4Item(Color4.Black, "Black"));
        _standardColors.Add(new Color4Item(Color4.Red, "Red"));
        _standardColors.Add(new Color4Item(Color4.Green, "Green"));
        _standardColors.Add(new Color4Item(Color4.Blue, "Blue"));
        _standardColors.Add(new Color4Item(Color4.Yellow, "Yellow"));
        _standardColors.Add(new Color4Item(Color4.Orange, "Orange"));
        _standardColors.Add(new Color4Item(Color4.Purple, "Purple"));
        return _standardColors;
    }

    private static ObservableCollection<Color4Item> CreateAvailableColors()
    {
        ObservableCollection<Color4Item> _standardColors = new();

        foreach (KeyValuePair<string, Color4?> item in Color4Utilities.KnownColors)
        {
            if (item.Value != null && !string.Equals(item.Key, "Transparent"))
            {
                Color4Item colorItem = new Color4Item(item.Value.Value, item.Key);

                if (!_standardColors.Contains(colorItem))
                {
                    _standardColors.Add(colorItem);
                }
            }
        }

        return _standardColors;
    }

    #endregion //Methods
}
