using Avalonia;
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
using System.Reactive;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;


namespace Homework_10_ColorPicker.Controls
{
    public class ColorPickerControl : TemplatedControl, INotifyPropertyChanged
    {
        private int indexColor;

        public static readonly StyledProperty<List<Color>> PrimaryColorsProperty = AvaloniaProperty.Register<ColorPickerControl, List<Color>>(nameof(PrimaryColors));
        public static readonly StyledProperty<ObservableCollection<Color>> AdditionalColorsProperty = AvaloniaProperty.Register<ColorPickerControl, ObservableCollection<Color>>(nameof(AdditionalColors));
        public static readonly StyledProperty<Color> CurrentColorProperty = AvaloniaProperty.Register<ColorPickerControl, Color>(nameof(CurrentColor), Colors.Aquamarine, defaultBindingMode:BindingMode.TwoWay);
        public static readonly StyledProperty<HsvColor> CurrentHsvColorProperty = AvaloniaProperty.Register<ColorPickerControl, HsvColor>(nameof(CurrentHsvColor), defaultValue: Colors.Aquamarine.ToHsv());
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

        public HsvColor CurrentHsvColor
        {
            get => GetValue(CurrentHsvColorProperty);
            set => SetValue(CurrentHsvColorProperty, value);
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

            CurrentHsvColor = (Color.Parse(CurrentColor.ToString())).ToHsv();

            GetPrimaryColor = ReactiveCommand.Create<IBrush, Unit>(brush =>
            {
                CurrentColor = Color.Parse(brush.ToString());

                return Unit.Default;
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

    public class RgbToBrushMultiConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {

            if (values?.Count != 4 || !targetType.IsAssignableFrom(typeof(Color)))
                throw new NotSupportedException();

            if (!values.All(x => x is string or UnsetValueType or null))
                return BindingOperations.DoNothing;

            if (!byte.TryParse((string?)values[0], out var r) ||
                !byte.TryParse((string?)values[1], out var g) ||
                !byte.TryParse((string?)values[2], out var b) ||
                !byte.TryParse((string?)values[3], out var a))

                return BindingOperations.DoNothing;

            var color = new Color(a, r, g, b);
            return color;
        }
    }

}
