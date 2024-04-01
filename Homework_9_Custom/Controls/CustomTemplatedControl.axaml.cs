using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using ReactiveUI;
using System.Reactive;
using System.Threading;
using System.Windows.Input;

namespace Homework_9_Custom.Controls
{
    public class CustomTemplatedControl : TemplatedControl
    {
        public static readonly StyledProperty<ICommand> ToggleButtonProperty = AvaloniaProperty.Register<CustomTemplatedControl, ICommand>(nameof(ToggleButton));

        public ICommand ToggleButton
        {
            get => GetValue(ToggleButtonProperty);
            set => SetValue(ToggleButtonProperty, value);
        }

        public static readonly StyledProperty<string> NamedButtonProperty = AvaloniaProperty.Register<CustomTemplatedControl, string>(nameof(NamedButton));

        public string NamedButton
        {
            get => GetValue(NamedButtonProperty);
            set => SetValue(NamedButtonProperty, value);
        }

        public static readonly StyledProperty<bool> IsOpenProperty = AvaloniaProperty.Register<CustomTemplatedControl, bool>(nameof(IsOpen));

        public bool IsOpen
        {
            get => GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly StyledProperty<int> MaxValueProperty = AvaloniaProperty.Register<CustomTemplatedControl, int>(nameof(MaxValue));
        public int MaxValue
        {
            get => GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static readonly StyledProperty<int> MinValueProperty = AvaloniaProperty.Register<CustomTemplatedControl, int>(nameof(MinValue));
        public int MinValue
        {
            get => GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public CustomTemplatedControl()
        {
            NamedButton = "+";

            ToggleButton = ReactiveCommand.Create(() => {
                IsOpen = !IsOpen;

                if (NamedButton == "+")
                {
                    NamedButton = "-";
                }
                else
                {
                    NamedButton = "+";
                }
            });
        }

    }
}
