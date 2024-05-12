using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework_LogicalApp.Controls
{
    public class Connectable : Control, ILogicalControl
    {
        public int Id { get; set; }
        public ObservableCollection<bool> BoolArrayIn { get; set; } = new ObservableCollection<bool>();
        public ObservableCollection<bool> BoolArrayOut { get; set; } = new ObservableCollection<bool>();

        public Connectable? output_el = null;
        public Connectable? input_el = null;

        public bool IsSelected { get; set; } = false;
        public Point Position { get; set; }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            BoolArrayOut = GetOutput(BoolArrayIn);
        }

        public Connectable(int id, Point? spawnPoint)
        {
            this.Id = id;
            this.Loaded += (s, e) =>
            {
                Bounds = new Rect(spawnPoint ?? new Point(0, 0), Bounds.Size);
                InvalidateVisual();
            };
        }

        public Connectable() { }

        public virtual void SetPosition(Point position)
        {
            Position = position;
            Bounds = new Rect(position, Bounds.Size);
            //InvalidateMeasure();
            InvalidateVisual();
        }

        public virtual ObservableCollection<bool> GetOutput(ObservableCollection<bool> input)
        {
            return new ObservableCollection<bool>();
        }
    }
}
