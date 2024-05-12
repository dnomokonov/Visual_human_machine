using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Input;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.VisualTree;
using System.Collections.ObjectModel;

namespace Homework_LogicalApp.Controls;

public partial class InputControler : Connectable, ILogicalControl
{
    private const double Radius = 5;
    private Point _positionInBlock;
    private TranslateTransform _transform = null!;
    
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public string SetFonts { get; set; }
    public double SizeFonts { get; set; }
    
    public InputControler(int id, Point? p = null) : base(id, p)
    {
        Width = 60;
        Height = 30;
        Stroke = Brushes.Black;
        StrokeThickness = 2;
        SetFonts = "Arial";
        SizeFonts = 20;
        
        BoolArrayOut = new ObservableCollection<bool>(Enumerable.Repeat(false, CountInput));
    }

    public sealed override void Render(DrawingContext context)
    {
        var renderSize = Bounds.Size;
        
        // Set Fonts for text
        var typeface = new Typeface(SetFonts);

        // Set outline color based on selection
        var outlineBrush = IsSelected ? Brushes.OrangeRed : Brushes.Black;
        var outlinePen = new Pen(outlineBrush, StrokeThickness);
        
        // Checking for the number of elements
        if (CountInput is <= 0 or > 5)
        {
            CountInput = 1;
        }

        // Draw form
        var d = CountInput * 20;
        var geometry = new StreamGeometry();
        using (var geometryContext = geometry.Open())
        {
            geometryContext.BeginFigure(new Point(0, 0), isFilled: true);
            geometryContext.LineTo(new Point(d, 0));
            geometryContext.LineTo(new Point(d + 20, renderSize.Height / 2));
            geometryContext.LineTo(new Point(d, renderSize.Height));
            geometryContext.LineTo(new Point(0, renderSize.Height)); 
            geometryContext.LineTo(new Point(0, 0));
        }
        context.DrawGeometry(Brushes.White, outlinePen, geometry);
        
        var x = d + 20;
        var y = renderSize.Height / 2;
        context.DrawEllipse(Brushes.Green, outlinePen, new Rect(x - Radius, y - Radius, Radius * 2, Radius * 2));
        
        // Show and Toggle Bool NumArray
        var interval = 10;
        var brushTrue = Brushes.LimeGreen;
        var brushFalse = Brushes.DarkGreen;
        for (var i = 0; i < CountInput; i++)
        {
            string boolText = BoolArrayOut != null && BoolArrayOut[i] ? "1" : "0";
            var colorText = BoolArrayOut != null && BoolArrayOut[i] ? brushTrue : brushFalse;

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


    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.PointerUpdateKind != PointerUpdateKind.LeftButtonPressed) return;
        
        double clickX = e.GetPosition(this).X;
        int clickedIndex = (int)Math.Floor((clickX - 10) / 18); 

        if (BoolArrayOut != null && clickedIndex >= 0 && clickedIndex < BoolArrayOut.Count)
        {
            BoolArrayOut[clickedIndex] = !BoolArrayOut[clickedIndex];
            InvalidateVisual();
        }
        
        e.Handled = true;
        _positionInBlock = e.GetPosition(Parent as Visual);
            
        if (_transform != null!) 
            _positionInBlock = new Point(
                _positionInBlock.X - _transform.X,
                _positionInBlock.Y - _transform.Y);
        
        
        InvalidateVisual();
        base.OnPointerPressed(e);
    }

    public override ObservableCollection<bool> GetOutput(ObservableCollection<bool> input)
    {
        return BoolArrayOut;
    }
}