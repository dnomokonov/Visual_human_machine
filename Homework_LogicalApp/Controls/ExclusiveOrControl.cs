using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Input;
using System.Collections.Generic;

namespace Homework_LogicalApp.Controls;

public class ExclusiveOrControl : Control
{
    private double _radius = 5;
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

    public ExclusiveOrControl()
    {
        Width = 50;
        Height = 100;
        Stroke = Brushes.Black;
        StrokeThickness = 1;
        SizeHeader = 20;
        SizeLabel = 18;
        HeaderValve = "=1";
        LabelValve = "XOR";
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
            context.DrawGeometry(null, outlinePen, pathGeometry);
            
            // Draw Lines (Up and Down)
            context.DrawLine(outlinePen, lineX, startX);
            context.DrawLine(outlinePen, lineY, endY);
            
            //Draw double wall
            var pathGeometry2 = new PathGeometry();
            var pathFigure2 = new PathFigure
            {
                StartPoint = new Point(0, 10),
                IsClosed = false,
                IsFilled = true
            };
            pathFigure2.Segments?.Add(new ArcSegment
            {
                Point = new Point(0, 80),
                Size = new Size(50, 50),
            });
            pathGeometry2.Figures?.Add(pathFigure2);
            context.DrawGeometry(Brushes.White, outlinePen, pathGeometry2);
            
            var pathGeometry3 = new PathGeometry();
            var pathFigure3 = new PathFigure
            {
                StartPoint = new Point(-10, 10),
                IsClosed = false,
                IsFilled = true
            };
            pathFigure3.Segments?.Add(new ArcSegment
            {
                Point = new Point(-10, 80),
                Size = new Size(50, 50),
            });
            pathGeometry3.Figures?.Add(pathFigure3);
            context.DrawGeometry(Brushes.White, outlinePen, pathGeometry3);
            
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
            var x1 = 4;
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
            
            string labelSize = LabelValve;
            int lsize = labelSize.Length;

            context.DrawText(labelText,
                lsize <= 4 ? new Point(lsize, posLabelY) : new Point(posLabelX - lsize * 2, posLabelY));

            // Draw left circle-input
            var x2 = renderSize.Width;
            var y2 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Red, outlinePen, new Rect(x2 - _radius, y2 - _radius, _radius * 2, _radius * 2));
            
            // Draw Circle-input
            // Problem: The maximum number of inputs is 6. It is no longer possible
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            double interval = 10;
            for (int i = 0; i < CountInput; i++)
            {
                if (CountInput <= 1)
                {
                    CountInput = 2;
                    i = 0;
                    continue;
                }
                
                if (i % 2 == 0)
                {
                    context.DrawEllipse(Brushes.Blue, outlinePen, new Rect(x1 - _radius, y1 - interval - _radius, _radius * 2, _radius * 2));
                }
                else
                {
                    context.DrawEllipse(Brushes.Blue, outlinePen, new Rect(x1 - _radius, y1 + interval - _radius, _radius * 2, _radius * 2));
                }

                interval += 9;
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