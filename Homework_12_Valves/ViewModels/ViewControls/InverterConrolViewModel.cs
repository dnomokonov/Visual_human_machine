using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Homework_12_Valves.ViewModels.ViewControls;
public class InverterConrolViewModel : Control
{
    private double _radius = 6;
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public string SetFonts { get; set; }
    public int CountInput { get; set; } // count of inputs 
    public int SizeHeader { get; set; }
    public string HeaderValve { get; set; }
    public int SizeLabel { get; set; }
    public string LabelValve { get; set; }
    public string TypeValve { get; set; } // Type: GOST or ANSI 

    public InverterConrolViewModel()
    {
        SizeHeader = 20;
        SizeLabel = 18;
        HeaderValve = "1";
        LabelValve = "Inverter";
        SetFonts = "Arial";
        TypeValve = "GOST";
        CountInput = 1;
    }
    
    public sealed override void Render(DrawingContext context)
    {
        var renderSize = Bounds.Size;
        
        // Set Fonts for text
        var typeface = new Typeface(SetFonts);
        
        if (TypeValve == "ANSI")
        {
            var centerX = renderSize.Width / 2;
            var centerY = renderSize.Height / 2;
            double sideLength = 100; // Length 
            
            // Coordinates of the vertices
            var point1 = new Point(0, 0); // Up
            var point2 = new Point(0, sideLength); // Down
            var point3 = new Point(centerX + sideLength / 2, centerY); // Right

            // Draw triangle-box
            context.DrawLine(new Pen(Stroke, StrokeThickness), point1, point2);
            context.DrawLine(new Pen(Stroke, StrokeThickness), point1, point3);
            context.DrawLine(new Pen(Stroke, StrokeThickness), point2, point3);
            
            // Draw Circle-output
            context.DrawEllipse(null, new Pen(Stroke, StrokeThickness), new Rect(centerX + sideLength / 2 - _radius - 2, centerY - _radius - 2, (_radius + 2) * 2, (_radius + 2) * 2));
            context.DrawEllipse(Brushes.Red, new Pen(Stroke, StrokeThickness), new Rect(centerX + sideLength / 2 - _radius, centerY - _radius, _radius * 2, _radius * 2));
            
            // Draw Circle-input
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Blue, new Pen(Stroke, StrokeThickness), new Rect(x1 - _radius, y1 - _radius, _radius * 2, _radius * 2));
            
            // Set Label valve
            // Problem: there is no adaptability to the length of the text
            var posLabelX = 0; // Value varies 
            var posLabelY = renderSize.Height + 5;
            var labelText = new FormattedText(
                LabelValve, 
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface,
                SizeLabel,
                Brushes.Black);
            context.DrawText(labelText, new Point(posLabelX, posLabelY));
            
            // Problem: The maximum number of inputs is 6. It is no longer possible
            /*
            double interval = 10;
            for (int i = 0; i < CountInput; i++)
            {
                if (i % 2 == 0)
                {
                    context.DrawEllipse(Brushes.Blue, new Pen(Stroke, StrokeThickness), new Rect(0 - _radius, sideLength / 2 - interval - _radius, _radius * 2, _radius * 2));
                }
                else
                {
                    context.DrawEllipse(Brushes.Blue, new Pen(Stroke, StrokeThickness), new Rect(0 - _radius, sideLength / 2 + interval - _radius, _radius * 2, _radius * 2));
                }

                interval += 9;
            }
            */
        }
        else
        {
            // Rectangle-Box 
            var rect = new Rect(renderSize);
            context.DrawRectangle(null, new Pen(Stroke, StrokeThickness), rect);
        
            // Set Header valve
            // Problem: there is no adaptability to the length of the text
            var posHeaderX = renderSize.Width / 3;
            var posHeaderY = 4; // Value varies 
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
            var posLabelX = 0; // Value varies 
            var posLabelY = renderSize.Height + 5;
            var labelText = new FormattedText(
                LabelValve, 
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface,
                SizeLabel,
                Brushes.Black);
            context.DrawText(labelText, new Point(posLabelX, posLabelY));
            
            // Draw left circle-input
            var x2 = renderSize.Width;
            var y2 = renderSize.Height / 2;
            context.DrawEllipse(null, new Pen(Stroke, StrokeThickness), new Rect(x2 - _radius - 2, y2 - _radius - 2, (_radius + 2) * 2, (_radius + 2) * 2));
            context.DrawEllipse(Brushes.Red, new Pen(Stroke, StrokeThickness), new Rect(x2 - _radius, y2 - _radius, _radius * 2, _radius * 2));
            
            // Draw Circle-input
            // Problem: The maximum number of inputs is 6. It is no longer possible
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Blue, new Pen(Stroke, StrokeThickness), new Rect(x1 - _radius, y1 - _radius, _radius * 2, _radius * 2));
            /*
            double interval = 10;
            for (int i = 0; i < CountInput; i++)
            {
                if (CountInput == 1)
                {
                    
                }
                
                if (i % 2 == 0)
                {
                    context.DrawEllipse(Brushes.Blue, new Pen(Stroke, StrokeThickness), new Rect(x1 - _radius, y1 - interval - _radius, _radius * 2, _radius * 2));
                }
                else
                {
                    context.DrawEllipse(Brushes.Blue, new Pen(Stroke, StrokeThickness), new Rect(x1 - _radius, y1 + interval - _radius, _radius * 2, _radius * 2));
                }

                interval += 9;
            }
            */
            
        }
        
        base.Render(context);
    }
}