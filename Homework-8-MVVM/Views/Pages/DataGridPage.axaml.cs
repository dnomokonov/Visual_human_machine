using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Homework_8_MVVM.ViewModels.Pages;

namespace Homework_8_MVVM.Views.Pages;

public partial class DataGridPage : UserControl
{
    public DataGridPage()
    {
        InitializeComponent();
        DataContext = new DataGridPageViewModel();
    }
}