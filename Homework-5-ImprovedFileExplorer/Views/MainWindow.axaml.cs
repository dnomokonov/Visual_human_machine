using Avalonia.Controls;
using Avalonia.Input;
using Homework_5_ImprovedFileExplorer.ViewModels;

namespace Homework_5_ImprovedFileExplorer.Views;

public partial class MainWindow : Window
{

    private readonly ExplorerFileManager ExplorerManager;

    public MainWindow()
    {
        InitializeComponent();
        ExplorerManager = new ExplorerFileManager();
        DataContext = ExplorerManager;
    }

    private void OnListBoxDoubleTapped(object? s, TappedEventArgs e)
    {
        if (e.Source is Control { DataContext: DataFileExplorer datacontext})
        {
            if (s is ListBox { DataContext: MainWindowViewModel viewModel})
            {
                viewModel.ExplorerManager.Selected = datacontext;
            }
        }
    }
}