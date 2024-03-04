using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData.Experimental;
using Path = System.IO.Path;

namespace Homework_5_ImprovedFileExplorer.ViewModels;

public class DataFileExplorer : ObservableObject
{
    private string? _nameFile;
    private string? _pathFile;
    private string? _typeFile;
    private Bitmap? _imagePath;

    public string? NameFile
    {
        get => _nameFile;
        set => SetProperty(ref _nameFile, value);
    }

    public string? PathFile
    {
        get => _pathFile;
        set => SetProperty(ref _pathFile, value);
    }

    public string? TypeFile
    {
        get => _typeFile;
        set => SetProperty(ref _typeFile, value);
    }

    public Bitmap? ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }
}

public class ExplorerFileManager : ObservableObject
{
    private ObservableCollection<DataFileExplorer> _files = new ObservableCollection<DataFileExplorer>();
    private string _currentPath = Directory.GetCurrentDirectory();
    private FileSystemWatcher _fileWatcher;
    private DataFileExplorer _selected;
    private Bitmap? _selectedImage;
    private int _statusShow = 0;

    public ObservableCollection<DataFileExplorer> Files
    {
        get => _files;
        set => SetProperty(ref _files, value);
    }

    public ExplorerFileManager()
    {
        LoadItemsAsync();
    }

    private async Task LoadItemsAsync()
    {
        _fileWatcher = new FileSystemWatcher(_currentPath);
        _fileWatcher.IncludeSubdirectories = false;
        _fileWatcher.EnableRaisingEvents = true;

        Files.Clear();

        _fileWatcher.Changed += FileWatcher_OnChanged;
        _fileWatcher.Created += FileWatcher_OnChanged;
        _fileWatcher.Deleted += FileWatcher_OnChanged;
        _fileWatcher.Renamed += FileWatcher_OnChanged;

        if (_statusShow == 0)
        {
            string parentDir = Path.GetDirectoryName(_currentPath);
            _files.Add(new DataFileExplorer { 
                NameFile = "..",
                TypeFile = "folder",
                PathFile = parentDir, 
                ImagePath = new Bitmap(AssetLoader.Open(new Uri("avares://Homework-5-ImprovedFileExplorer/Assets/forderUp.png"))) 
            });

            await LoadDirectoriesAsync(_currentPath);
            await LoadFilesAsync(_currentPath);
        }
        else if (_statusShow == 1)
        {
            await LoadHardDiskAsync();
        }
    }

    private async Task LoadDirectoriesAsync(string path)
    {
        await Task.Run(() =>
        {
            if (Files == null) { Files = new ObservableCollection<DataFileExplorer>(); }

            foreach (var d in Directory.GetDirectories(_currentPath))
            {
                Files.Add(new DataFileExplorer 
                {
                    NameFile = Path.GetFileName(d),
                    PathFile = d,
                    TypeFile = "folder",
                    ImagePath = new Bitmap(AssetLoader.Open(new Uri("avares://Homework-5-ImprovedFileExplorer/Assets/folder.png"))),
                });
            }

        });
    }

    private async Task LoadFilesAsync(string path)
    {
        await Task.Run(() =>
        {
            if (Files == null) { Files = new ObservableCollection<DataFileExplorer>(); }

            foreach (var f in Directory.GetFiles(_currentPath))
            {
                try
                {
                    using (var bmp = new Bitmap(f))
                    {
                        Files.Add(new DataFileExplorer
                        {
                            NameFile = Path.GetFileName(f),
                            PathFile = f,
                            TypeFile = Path.GetExtension(f),
                            ImagePath = new Bitmap(AssetLoader.Open(new Uri("avares://Homework-5-ImprovedFileExplorer/Assets/imagefile.png"))),
                        });
                    }
                }
                catch { }
            }

        });
    }

    private async Task LoadHardDiskAsync()
    {
        await Task.Run(() =>
        {
            if (Files == null) { Files = new ObservableCollection<DataFileExplorer>(); }

            foreach (var disk in DriveInfo.GetDrives())
            {
                Files.Add(new DataFileExplorer
                {
                    NameFile = disk.Name,
                    PathFile = disk.RootDirectory.ToString(),
                    TypeFile = "hdisk",
                    ImagePath = new Bitmap(AssetLoader.Open(new Uri("avares://Homework-5-ImprovedFileExplorer/Assets/hardisk.png")))
                });
            }
        });
    }

    
    private async void FileWatcher_OnChanged(object s, FileSystemEventArgs e)
    {
        switch (e.ChangeType)
        {
            case WatcherChangeTypes.Changed:
                await LoadItemsAsync();
                break;
            case WatcherChangeTypes.Created:
                await LoadItemsAsync();
                break;
            case WatcherChangeTypes.Deleted:
                await LoadItemsAsync();
                break;
            case WatcherChangeTypes.Renamed:
                await LoadItemsAsync();
                break;
        }
    }

    public DataFileExplorer Selected
    {
        get => _selected;
        set
        {
            if (value != null && (value.TypeFile == "folder" || value.TypeFile == "hdisk"))
            {
                _selected = value;
                Add(_selected);
            }
            else if (value != null && value.TypeFile != "folder" && value.TypeFile != "hdisk")
            {
                try
                {
                    SelectedImage = new Bitmap(value.PathFile);
                }
                catch
                {
                    SelectedImage = null;
                }
            }
        }
    }

    private void Add(DataFileExplorer paths)
    {
        if (!string.IsNullOrEmpty(paths.PathFile))
        {
            _currentPath = paths.PathFile;
            _statusShow = 0;
        }
        else if (paths.NameFile == ".." && Directory.GetParent(_currentPath) == null)
        {
            _statusShow = 1;
        }
        
        LoadItemsAsync();
    }

    public Bitmap? SelectedImage
    {
        get => _selectedImage;
        set
        {
            if (_selectedImage != value)
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }
    }
}

public class MainWindowViewModel : ViewModelBase
{
    public ExplorerFileManager ExplorerManager { get; }

    public MainWindowViewModel()
    {
        ExplorerManager = new ExplorerFileManager();
    }
}
