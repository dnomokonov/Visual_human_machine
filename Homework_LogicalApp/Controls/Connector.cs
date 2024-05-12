using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;

namespace Homework_LogicalApp.Controls
{
    public class Connector : Connectable
    {
        public Connector(int id, Point pos) : base(id, pos) { }
        public Connector(int id, Connectable input, Connectable output) : base(id, new Point(0, 0))
        {
            this.Id = id;   

            input_el = input;
            input_el.output_el = output;    
            output_el = output;
            output_el.input_el = input;

            Position = new Point(0, 0);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            context.DrawLine(new Pen(Brushes.LimeGreen, 2), input_el.Position, output_el.Position);
        }

        public void SetPosition(double x, double y)
        {
            Position = new Point(x, y);
        }

        public Point GetPosition()
        {
            return Position;
        }
    }
}
