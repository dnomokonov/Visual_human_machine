﻿using System;
using System.Globalization;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace Homework_LogicalApp.Controls;

public class InverterConrol : Control
{
    private double _radius = 4;
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

    public InverterConrol()
    {
        Width = 50;
        Height = 100;
        Stroke = Brushes.Black;
        StrokeThickness = 1;
        SizeHeader = 20;
        SizeLabel = 18;
        HeaderValve = "1";
        LabelValve = "Inverter";
        SetFonts = "Arial";
        TypeValve = "GOST";
        CountInput = 1;
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
            var centerX = renderSize.Width / 2;
            var centerY = renderSize.Height / 2;
            double sideLength = 100; // Length 
            
            // Coordinates of the vertices
            var point1 = new Point(0, 0); // Up
            var point2 = new Point(0, sideLength); // Down
            var point3 = new Point(centerX + sideLength / 2, centerY); // Right

            // Draw triangle-box
            context.DrawLine(outlinePen, point1, point2);
            context.DrawLine(outlinePen, point1, point3);
            context.DrawLine(outlinePen, point2, point3);
            
            // Draw Circle-output
            context.DrawEllipse(null, outlinePen, new Rect(centerX + sideLength / 2 - _radius - 2, centerY - _radius - 2, (_radius + 2) * 2, (_radius + 2) * 2));
            context.DrawEllipse(Brushes.Red, outlinePen, new Rect(centerX + sideLength / 2 - _radius, centerY - _radius, _radius * 2, _radius * 2));
            
            // Draw Circle-input
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Blue, outlinePen, new Rect(x1 - _radius, y1 - _radius, _radius * 2, _radius * 2));
            
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
                lsize <= 4 ? new Point(lsize, posLabelY) : new Point(posLabelX - lsize * 2 + 10, posLabelY));
            
            // Draw left circle-input
            var x2 = renderSize.Width;
            var y2 = renderSize.Height / 2;
            context.DrawEllipse(null, outlinePen, new Rect(x2 - _radius - 2, y2 - _radius - 2, (_radius + 2) * 2, (_radius + 2) * 2));
            context.DrawEllipse(Brushes.Red, outlinePen, new Rect(x2 - _radius, y2 - _radius, _radius * 2, _radius * 2));
            
            // Draw Circle-input
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Blue, outlinePen, new Rect(x1 - _radius, y1 - _radius, _radius * 2, _radius * 2));
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