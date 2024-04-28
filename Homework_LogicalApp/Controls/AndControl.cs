using System;
using System.Globalization;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace Homework_LogicalApp.Controls;

public class AndControl : Control
{
     private readonly double _radius = 4;
     private bool _isSelected;
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public string SetFonts { get; set; }
    public int CountInput { get; set; } // count of inputs 
    public int SizeHeader { get; set; }
    public string HeaderValve { get; set; }
    public int SizeLabel { get; set; }
    public string LabelValve { get; set; }
    public string TypeValve { get; set; } // Type: GOST or ANSI
    public List<bool> InputStates { get; set; }
    
    
    public AndControl()
    {
        Width = 50;
        Height = 100;
        Stroke = Brushes.Black;
        StrokeThickness = 1;
        SizeHeader = 20;
        SizeLabel = 18;
        HeaderValve = "&";
        LabelValve = "AND";
        SetFonts = "Arial";
        TypeValve = "GOST";
        CountInput = 2;
        InputStates = new List<bool>();
    }
    
    public sealed override void Render(DrawingContext context)
    {
        var renderSize = Bounds.Size;
        
        // Set Fonts for text
        var typeface = new Typeface(SetFonts);
        
        // Set outline color based on selection
        var outlineBrush = _isSelected ? Brushes.OrangeRed : Brushes.Black;
        var outlinePen = new Pen(outlineBrush, StrokeThickness);
        
        if (TypeValve == "ANSI")
        {
            // Point semicircle
            var startX = new Point(35, 10);
            var endY = new Point(35, 80);
            var lineX = new Point(0, 10);
            var lineY = new Point(0, 80);
            
            // Draw semicircle
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure
            {
                StartPoint = startX,
                IsClosed = false,
                IsFilled = true
            };
            pathFigure.Segments?.Add(new ArcSegment
            {
                Point = endY,
                Size = new Size(1, 1),
            });
            pathGeometry.Figures?.Add(pathFigure);
            context.DrawGeometry(Brushes.White, outlinePen, pathGeometry);
            
            // Draw Lines (Up and Down)
            context.DrawLine(outlinePen, lineX, startX);
            context.DrawLine(outlinePen, lineY, endY);
            context.DrawLine(outlinePen, lineX, lineY);
            
            // Set Label valve
            var posLabelX = 0; // Value varies 
            var posLabelY = renderSize.Height - 15;
            var labelText = new FormattedText(
                LabelValve, 
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface,
                SizeLabel,
                outlineBrush);
            
            string labelSize = LabelValve;
            int lsize = labelSize.Length;

            context.DrawText(labelText,
                lsize <= 4 ? new Point(lsize, posLabelY) : new Point(posLabelX - lsize * 2, posLabelY));
            
            // Draw input-point
            var x1 = 0;
            var y1 = 42;
            double interval = 6;
            for (int i = 0; i < CountInput; i++)
            {
                // if there are fewer inputs than required
                if (CountInput <= 1)
                {
                    CountInput = 2;
                    i = 0;
                    continue;
                }

                context.DrawEllipse(Brushes.Blue, outlinePen,
                    i % 2 == 0
                        ? new Rect(x1 - _radius, y1 - interval - _radius, _radius * 2, _radius * 2)
                        : new Rect(x1 - _radius, y1 + interval - _radius, _radius * 2, _radius * 2));

                interval += 8;
            }
            
            // Draw output-point
            var x2 = 70;
            var y2 = 44;
            context.DrawEllipse(Brushes.Red, outlinePen, new Rect(x2 - _radius, y2 - _radius, _radius * 2, _radius * 2));
        }
        else
        {
            // Rectangle-Box 
            var rect = new Rect(renderSize);
            context.DrawRectangle(Brushes.White, outlinePen, rect);
        
            // Set Header valve
            var posHeaderX = renderSize.Width / 3;
            var posHeaderY = 4; // Value varies 
            var headerText = new FormattedText(
                HeaderValve, 
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface,
                SizeHeader,
                outlineBrush);
            context.DrawText(headerText, new Point(posHeaderX, posHeaderY));
        
            // Set Label valve
            var posLabelX = 0; // Value varies 
            var posLabelY = renderSize.Height + 5;
            var labelText = new FormattedText(
                LabelValve, 
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface,
                SizeLabel,
                outlineBrush);
            context.DrawText(labelText, new Point(posLabelX, posLabelY));
            
            // Draw left circle-input
            var x2 = renderSize.Width;
            var y2 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Red, outlinePen, new Rect(x2 - _radius, y2 - _radius, _radius * 2, _radius * 2));
            
            // Draw Circle-input
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            
            double interval = 6;
            for (int i = 0; i < CountInput; i++)
            {
                // if there are fewer inputs than required
                if (CountInput <= 1)
                {
                    CountInput = 2;
                    i = 0;
                    continue;
                }

                context.DrawEllipse(Brushes.Blue, outlinePen,
                    i % 2 == 0
                        ? new Rect(x1 - _radius, y1 - interval - _radius, _radius * 2, _radius * 2)
                        : new Rect(x1 - _radius, y1 + interval - _radius, _radius * 2, _radius * 2));

                interval += 6;
            }
        }
        
        base.Render(context);
    }
    
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
            
        var point = e.GetPosition(this);
        if (Bounds.Contains(point))
        {
            _isSelected = !_isSelected;
            InvalidateVisual();
        }
    }
}