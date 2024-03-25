using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Homework_8_MVVM.Models;
using Avalonia.Controls;
using System.Collections.Generic;
using Homework_8_MVVM.ViewModels.Pages;
using System.Reflection.Metadata;
using DynamicData;
using System.Reactive;

namespace Homework_8_MVVM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private object? content;
    private ObservableCollection<BaseViewModel> vmbaseCollection;

    public ReactiveCommand<BaseViewModel, Unit> ChangeViewCommand { get; }

    public MainWindowViewModel()
    {
        vmbaseCollection = new ObservableCollection<BaseViewModel>();
        vmbaseCollection.Add(new DataGridPageViewModel());
        vmbaseCollection.Add(new TreeViewPageViewModel());

        Content = null;

        ChangeViewCommand = ReactiveCommand.Create<BaseViewModel>(ChangeView);
    }

    private void ChangeView(BaseViewModel viewModel)
    {
        Content = viewModel;
    }

    public object? Content
    {
        get => content;
        set
        {
            this.RaiseAndSetIfChanged(ref content, value);
        }
    }

    public ObservableCollection<BaseViewModel> VmbaseCollection
    {
        get => vmbaseCollection;
        set
        {
            this.RaiseAndSetIfChanged(ref vmbaseCollection, value);
        }
    }
}