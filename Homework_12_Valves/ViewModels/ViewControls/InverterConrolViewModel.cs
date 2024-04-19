using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Homework_12_Valves.ViewModels.ViewControls;
public class InverterConrolViewModel : Control
{
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public int CountLines { get; set; } // count of inputs 
    public int SizeHeader { get; set; }
    public string HeaderValve { get; set; }
    public int SizeLabel { get; set; }
    public string LabelValve { get; set; }

    public InverterConrolViewModel()
    {
        SizeHeader = 20;
        SizeLabel = 18;
        HeaderValve = "1";
        LabelValve = "Inverter";
    }
    
    public sealed override void Render(DrawingContext context)
    {
        // Rectangle-Box 
        var renderSize = Bounds.Size;
        var rect = new Rect(renderSize);
        context.DrawRectangle(null, new Pen(Stroke, StrokeThickness), rect);

        // Set Fonts for text
        var typeface = new Typeface("Arial");
        
        // Set Header valve
        // Problem: there is no adaptability to the length of the text
        var posHeaderX = renderSize.Width / 3;
        var posHeaderY = 4;
        var headerText = new FormattedText(
            HeaderValve, 
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            typeface,
            SizeHeader,
            Brushes.Black);
        context.DrawText(headerText, new Point(posHeaderX, posHeaderY));
        
        // Set Label valve
        // Problem: there is no adaptability to the length of the text
        var posLabelX = -SizeLabel / 3;
        var posLabelY = renderSize.Height + 5;
        var labelText = new FormattedText(
            LabelValve, 
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            typeface,
            SizeLabel,
            Brushes.Black);
        context.DrawText(labelText, new Point(posLabelX, posLabelY));
        
        // Draw right line-input
        var x1 = 0;
        var y1 = renderSize.Height / 2;
        context.DrawLine(new Pen(Stroke, StrokeThickness), new Point(x1, y1), new Point(x1 - 50, y1));
        
        // Draw left line-output
        var x2 = renderSize.Width;
        var y2 = renderSize.Height / 2;
        var radius = 8;
        context.DrawEllipse(null, new Pen(Stroke, StrokeThickness), new Rect(x2 - radius, y2 - radius, radius * 2, radius * 2));
        context.DrawLine(new Pen(Stroke, StrokeThickness), new Point(x2, y2), new Point(x2 + 50, y2));
        
        base.Render(context);
    }
}