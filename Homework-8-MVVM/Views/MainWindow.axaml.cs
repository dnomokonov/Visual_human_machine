using Avalonia.Controls;
using Homework_8_MVVM.ViewModels;
using Homework_8_MVVM.Views.Pages;

namespace Homework_8_MVVM.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}