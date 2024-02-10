using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Homework_2_AvaloniaColor.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        if (sender is Button button)
        {
            rectangle.Fill = button.Background;
        }
    }
}
