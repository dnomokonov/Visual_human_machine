using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ReactiveUI;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;

namespace Homework_4_Explorer.ViewModels;

public class DirCollection : ObservableObject
{
    private string? _name;
    private string? _pathFile;
    private bool _isDirectory;
    private Bitmap? _imagePath;

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string? PathFile
    {
        get => _pathFile;
        set => SetProperty(ref _pathFile, value);
    }

    public bool IsDirectory
    {
        get => _isDirectory;
        set => SetProperty(ref _isDirectory, value);
    }

    public Bitmap? ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }
}

public class ExplorerFileMenager : ObservableObject
{
    private ObservableCollection<DirCollection>? _directories;
    private string _currentpath = Directory.GetCurrentDirectory();
    private DirCollection? _selectedDir;

    public ObservableCollection<DirCollection>? Directories
    {
        get => _directories;
        set => SetProperty(ref _directories, value);
    }
    
    public ExplorerFileMenager()
    {
        
        showDirectory();
    }

    private void showDirectory()
    {
        Directories = new ObservableCollection<DirCollection>();
        string? previousPath = Directory.GetParent(_currentpath)?.FullName;

        if (previousPath != null)
        {
            Directories.Add(new DirCollection { Name = "..", PathFile = previousPath, IsDirectory = true, ImagePath = new Bitmap("~../../../../../Assets/folder.png") });
        }

        foreach (var d in Directory.GetDirectories(_currentpath))
        {
            Directories.Add(new DirCollection { Name = Path.GetFileName(d), PathFile = d, IsDirectory = true, ImagePath = new Bitmap("~../../../../../Assets/folder.png") });
        }

        foreach (var f in Directory.GetFiles(_currentpath))
        {
            Directories.Add(new DirCollection { Name = Path.GetFileName(f), PathFile = f, IsDirectory = false, ImagePath = new Bitmap("~../../../../../Assets/file.png") });
        }

    }

    private void ShowDrives()
    {
        Directories = new ObservableCollection<DirCollection>();

        foreach (var disk in DriveInfo.GetDrives())
        {
            Directories.Add(new DirCollection { Name = disk.Name, PathFile = disk.RootDirectory.ToString(), IsDirectory = true, ImagePath = new Bitmap("~../../../../../Assets/hardisk.png") });
        }
    }

    public DirCollection? SelectedDir
    {
        get => _selectedDir;
        set
        {
            if (_selectedDir != value && value != null && value.IsDirectory == true)
            {
                _selectedDir = value;
                Add(_selectedDir);
            }
        }
    }

    private void Add(DirCollection newpath)
    {
        if (newpath == null || newpath.PathFile == null)
        {
            return;
        }

        if (newpath.IsDirectory == true && !string.IsNullOrEmpty(newpath.PathFile))
        {
            _currentpath = newpath.PathFile;
        }

        showDirectory();
    }
}

public class MainWindowViewModel : ViewModelBase
{
    public ExplorerFileMenager Explorer { get; }

    public MainWindowViewModel()
    {
        Explorer = new ExplorerFileMenager();
    }
}