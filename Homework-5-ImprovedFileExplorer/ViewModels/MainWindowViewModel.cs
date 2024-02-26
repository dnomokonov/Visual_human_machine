using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Homework_5_ImprovedFileExplorer.ViewModels;

public class CollectionFile : ObservableObject {
    private string? _namefile;
    private string? _filepath;
    private Bitmap? _iconfile;

    public string? Namefile
    {
        get => _namefile;
        set => SetProperty(ref _namefile, value);
    }

    public string? Filepath
    {
        get => _filepath;
        set => SetProperty(ref _filepath, value);
    }

    public Bitmap? Iconfile
    {
        get => _iconfile;
        set => SetProperty(ref _iconfile, value);
    }
}

public class ExplorerFileMenager : ObservableObject
{
    private ObservableCollection<CollectionFile>? _collectionfiles = new ObservableCollection<CollectionFile>();
    private string _currentpath = Directory.GetCurrentDirectory();
    private int _statusShow = 0;
    private CollectionFile? _sellectedDir;

    public ExplorerFileMenager()
    {
        Show();
    }

    public ObservableCollection<CollectionFile>? Collectionfiles
    {
        get => _collectionfiles;
        set => SetProperty(ref _collectionfiles, value);
    }

    public void Show()
    {

    }

}

public class MainWindowViewModel : ViewModelBase
{
    
}
