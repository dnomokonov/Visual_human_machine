using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Homework_8_MVVM.ViewModels.Pages;

namespace Homework_8_MVVM.Views.Pages;

public partial class TreeViewPage : UserControl
{
    public TreeViewPage()
    {
        InitializeComponent();
        DataContext = new TreeViewPageViewModel();
    }
}