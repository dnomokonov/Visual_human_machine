using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Input;
using System.Collections.Generic;
using Avalonia.Controls.Shapes;
using System.Globalization;
using Avalonia.LogicalTree;
using Avalonia.Controls.ApplicationLifetimes;

namespace Homework_LogicalApp.Controls;

public class OutputControler : Connectable
{
    public int CountInput { get; set; } = 1;
    public double SizeFonts { get; set; } = 20;
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public string SetFonts { get; set; }
    
    private const double Radius = 5;

    public OutputControler(int id, Point? p = null) : base(id, p)
    {
        Width = 60;
        Height = 30;
        Stroke = Brushes.Black;
        StrokeThickness = 2;
        SetFonts = "Arial";
    }

    public sealed override void Render(DrawingContext context)
    {
        var renderSize = Bounds.Size;
        
        // Set Fonts for text
        var typeface = new Typeface(SetFonts);

        // Set outline color based on selection
        var outlineBrush = IsSelected ? Brushes.OrangeRed : Brushes.Black;
        var outlinePen = new Pen(outlineBrush, StrokeThickness);

        // Draw form
        var geometry = new StreamGeometry();
        using (var geometryContext = geometry.Open())
        {
            geometryContext.BeginFigure(new Point(0, 0), isFilled: true);
            geometryContext.LineTo(new Point(renderSize.Width, 0));
            geometryContext.LineTo(new Point(renderSize.Width + 20, renderSize.Height / 2));
            geometryContext.LineTo(new Point(renderSize.Width, renderSize.Height));
            geometryContext.LineTo(new Point(0, renderSize.Height)); 
            geometryContext.LineTo(new Point(0, 0));
        }
        context.DrawGeometry(Brushes.White, outlinePen, geometry);

        var x = 0;
        var y = renderSize.Height / 2;
        context.DrawEllipse(Brushes.Green, outlinePen, new Rect(x - Radius, y - Radius, Radius * 2, Radius * 2));

        // Show and Toggle Bool NumArray
        var interval = 10;
        var brushTrue = Brushes.LimeGreen;
        var brushFalse = Brushes.DarkGreen;

        if (input_el == null || input_el is not ILogicalControl input_control) return;

        BoolArrayIn = input_control.GetOutput(input_el.BoolArrayIn);

        for (var i = 0; i < BoolArrayIn.Count; i++)
        {
            string boolText = BoolArrayIn[i] ? "1" : "0";
            var colorText = BoolArrayIn[i] ? brushTrue : brushFalse;

            var ftext = new FormattedText(
                boolText,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface,
                SizeFonts,
                Brushes.White
            );

            context.DrawEllipse(colorText, null,
                new Rect(new Point(interval - 2, renderSize.Height / 6), new Size(15, 22)));

            context.DrawText(ftext, new Point(interval, renderSize.Height / 6));

            interval += 18;
        }
    }
    

}