using Avalonia.Controls;
using Avalonia.Input;
using Homework_4_Explorer.ViewModels;

namespace Homework_4_Explorer.Views;

public partial class MainWindow : Window
{

    private readonly ExplorerFileMenager Explorer;

    public MainWindow()
    {
        InitializeComponent();
        Explorer = new ExplorerFileMenager();
        DataContext = Explorer;
    }

    private void OnListBoxDoubleTapped(object? sender, TappedEventArgs e)
    {
        if (e.Source is Control { DataContext: DirCollection datacontext })
        {
            if (sender is ListBox { DataContext: MainWindowViewModel viewModel })
            {
                viewModel.Explorer.SelectedDir = datacontext;
            }
        }
    }

}