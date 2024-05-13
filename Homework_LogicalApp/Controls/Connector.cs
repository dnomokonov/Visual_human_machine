using Avalonia;
using Avalonia.Media;
using System;

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

            Position = new Point(
                (input_el.Position.X + output_el.Position.X) / 2,
                (input_el.Position.Y + output_el.Position.Y) / 2
            );
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            Point inputCenter = new Point(input_el.Bounds.Right, input_el.Bounds.Center.Y);
            Point outputCenter = new Point(output_el.Bounds.Left, output_el.Bounds.Center.Y);


            double midX = (inputCenter.X + outputCenter.X) / 2;
            Point mid1 = new Point(midX, inputCenter.Y);
            Point mid2 = new Point(midX, outputCenter.Y);


            context.DrawLine(new Pen(Brushes.DarkGreen, 2), inputCenter, mid1);
            context.DrawLine(new Pen(Brushes.DarkGreen, 2), mid1, mid2);
            context.DrawLine(new Pen(Brushes.DarkGreen, 2), mid2, outputCenter);
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