using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace Homework_11_CustomTable.Controls
{
    public class TableControl : TemplatedControl
    {
        public static readonly StyledProperty<object> TargetObjectProperty = AvaloniaProperty.Register<TableControl, object>(nameof(TargetObject), null, defaultBindingMode: BindingMode.TwoWay);

        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            TableControlViewModel visualizer = new TableControlViewModel();
            visualizer.ParseObject(TargetObject);
            visualizer.FormContent(null, visualizer.Nodes);

            DataContext = visualizer;
        }
    }
}
