using Homework_LogicalApp.Controls;
using System.Collections.ObjectModel;

namespace Homework_LogicalApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public static int ComponentCount = 0;
    public ObservableCollection<Connectable> Items { get; set; } = new ObservableCollection<Connectable>();
    public ObservableCollection<Connectable> Examples { get; set; } = new ObservableCollection<Connectable>()
    {
        new InputControler(GetId(), new Avalonia.Point(0, 5)),
        new OutputControler(GetId(),new Avalonia.Point(0, 5)),
        new BufferControl(GetId(), new Avalonia.Point(10, 0)),
        new InverterConrol(GetId(), new Avalonia.Point(10, 0)),
        new AndControl(GetId(), new Avalonia.Point(10, 0)),
        new AndNotControl(GetId(), new Avalonia.Point(10, 0)),
        new OrControl(GetId(), new Avalonia.Point(10, 0)),
        new OrNotControl(GetId(), new Avalonia.Point(10, 0)),
        new ExclusiveOrControl(GetId(), new Avalonia.Point(10, 0)),
        new ExclusiveOrNotControl(GetId(), new Avalonia.Point(10, 0))
    };

    public static int GetId()
    {
        return ComponentCount++;
    }

    public MainWindowViewModel() { }    
}
