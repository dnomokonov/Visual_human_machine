using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Homework3.Views
{
    public partial class MainWindow : Window
    {
       public Calculator Calculator { get; } = new Calculator();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = Calculator;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    } 
}
