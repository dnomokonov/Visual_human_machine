using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Avalonia.Media;
using System.Linq;
using Avalonia.Data.Converters;
using System.Globalization;
using System;
using Avalonia.Data;
using System.Windows.Input;
using ReactiveUI;
using System.Reflection.Metadata;
using Avalonia.VisualTree;

namespace Homework_10_ColorPicker.Controls
{
    public class ColorPickerControl : TemplatedControl
    {
        private int indexColor;

        public static readonly StyledProperty<List<Color>> PrimaryColorsProperty = AvaloniaProperty.Register<ColorPickerControl, List<Color>>(nameof(PrimaryColors));
        public static readonly StyledProperty<ObservableCollection<Color>> AdditionalColorsProperty = AvaloniaProperty.Register<ColorPickerControl, ObservableCollection<Color>>(nameof(AdditionalColors));
        public static readonly StyledProperty<Color> CurrentColorProperty = AvaloniaProperty.Register<ColorPickerControl, Color>(nameof(CurrentColor), Colors.Aquamarine, defaultBindingMode:BindingMode.TwoWay);
        public static readonly StyledProperty<ICommand> AddColorProperty = AvaloniaProperty.Register<ColorPickerControl, ICommand>(nameof(AddColor));
        public static readonly StyledProperty<ICommand> GetPrimaryColorProperty = AvaloniaProperty.Register<ColorPickerControl, ICommand>(nameof(GetPrimaryColor));

        public List<Color> PrimaryColors
        {
            get => GetValue(PrimaryColorsProperty);
            set => SetValue(PrimaryColorsProperty, value);
        }

        public ObservableCollection<Color> AdditionalColors
        {
            get => GetValue(AdditionalColorsProperty);
            set => SetValue(AdditionalColorsProperty, value);
        }

        public Color CurrentColor
        {
            get => GetValue(CurrentColorProperty);
            set => SetValue(CurrentColorProperty, value);
        }

        public ICommand GetPrimaryColor
        {
            get => GetValue(GetPrimaryColorProperty);
            set => SetValue(GetPrimaryColorProperty, value);
        }

        public ICommand AddColor
        {
            get => GetValue(AddColorProperty);
            set => SetValue(AddColorProperty, value);
        }

        public ColorPickerControl()
        {
            indexColor = 0;

            PrimaryColors = new List<Color>(new[] {
                "#FF8080", "#FFFF80", "#80FF80", "#00FF80", "#80FFFF", "#0080FF", "#FF80C0", "#FF80FF",
                "#FF0000", "#FFFF00", "#80FF00", "#00FF40", "#00FFFF", "#0080C0", "#8080C0", "#FF00FF",
                "#804040", "#FF8040", "#00FF00", "#008080", "#004080", "#8080FF", "#800040", "#FF0080",
                "#800000", "#FF8000", "#008000", "#008040", "#0000FF", "#0000A0", "#800080", "#8000FF",
                "#400000", "#804000", "#004000", "#004040", "#000080", "#000040", "#400040", "#400080",
                "#000000", "#808000", "#808040", "#808080", "#408080", "#C0C0C0", "#400040", "#FFFFFF"
            }.Select(Color.Parse));
            
            AdditionalColors = new ObservableCollection<Color>(Enumerable.Repeat(Color.Parse("#FFFFFF"), 32));

            AddColor = ReactiveCommand.Create(() =>
            {
                if (indexColor == 32)
                {
                    indexColor = 0;
                }
                AdditionalColors[indexColor] = CurrentColor;
                indexColor++;
            });

        }

    }


    public class ConverterColorToBrush : IValueConverter
    {
        public object Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is Color color)
            {
                Console.WriteLine(color);
                return new SolidColorBrush(color);
            }
            return AvaloniaProperty.UnsetValue;
        }

        public object ConvertBack(object? value, Type target, object? parameter, CultureInfo? culture)
        {
            if (value is SolidColorBrush brush) {
                return brush.Color;
            }
            return AvaloniaProperty.UnsetValue;
        }

    }

}
