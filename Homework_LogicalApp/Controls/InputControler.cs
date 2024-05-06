using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Input;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Homework_LogicalApp.Controls;

public class InputControler : Connector
{
    private const double Radius = 5;
    private bool _isSelected;
    private bool _isPressed; 
    private Point _positionInBlock;
    private TranslateTransform _transform = null!;
    public int CountInput { get; set; }
    public List<bool>? BoolArray { get; set; }
    
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public string SetFonts { get; set; }
    public double SizeFonts { get; set; }
    
    public InputControler()
    {
        Width = 60;
        Height = 30;
        Stroke = Brushes.Black;
        StrokeThickness = 2;
        SetFonts = "Arial";
        SizeFonts = 20;
        _isSelected = false;
        CountInput = 3;
        
        BoolArray = new List<bool>(Enumerable.Repeat(false, CountInput));
    }

    public sealed override void Render(DrawingContext context)
    {
        var renderSize = Bounds.Size;
        
        // Set Fonts for text
        var typeface = new Typeface(SetFonts);

        // Set outline color based on selection
        var outlineBrush = _isSelected ? Brushes.OrangeRed : Brushes.Black;
        var outlinePen = new Pen(outlineBrush, StrokeThickness);
        
        // Checking for the number of elements
        if (CountInput is <= 0 or > 7)
        {
            CountInput = 1;
        }

        // Draw form
        var d = CountInput * 20;
        var geometry = new StreamGeometry();
        using (var geometryContext = geometry.Open())
        {
            geometryContext.BeginFigure(new Point(0, 0), isFilled: true);
            geometryContext.LineTo(new Point( d, 0));
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
            string boolText = BoolArray != null && BoolArray[i] ? "1" : "0";
            var colorText = BoolArray != null && BoolArray[i] ? brushTrue : brushFalse;

            var ftext = new FormattedText(
                boolText,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface,
                SizeFonts,
                Brushes.White
            );
            
            context.DrawEllipse(colorText, null, new Rect(interval - 2, renderSize.Height / 6, 15, 22));
            context.DrawText(ftext,new Point(interval, renderSize.Height / 6));

            interval += 18;
        }
        
    }
    
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.PointerUpdateKind != PointerUpdateKind.LeftButtonPressed) return;
        
        //_isSelected = !_isSelected; // Temporarily commented out
        
        double clickX = e.GetPosition(this).X;
        int clickedIndex = (int)Math.Floor((clickX - 10) / 18); // Assuming the interval is 18 and the first element starts at position 10
        
        e.Handled = true;
        _isPressed = true;
        _positionInBlock = e.GetPosition(Parent as Visual);
            
        if (_transform != null!) 
            _positionInBlock = new Point(
                _positionInBlock.X - _transform.X,
                _positionInBlock.Y - _transform.Y);
        
        if (BoolArray != null && clickedIndex >= 0 && clickedIndex < BoolArray.Count)
        {
            BoolArray[clickedIndex] = !BoolArray[clickedIndex];
            InvalidateVisual();
        }
        
        if (e.ClickCount == 2)
        {
            Connect(this);
        }
        
        InvalidateVisual();
        base.OnPointerPressed(e);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        _isPressed = false;
            
        base.OnPointerReleased(e);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        if (!_isPressed)
            return;
            
        if (Parent == null)
            return;

        var currentPosition = e.GetPosition(Parent as Visual);
        var offsetX = currentPosition.X -  _positionInBlock.X;
        var offsetY = currentPosition.Y - _positionInBlock.Y;

        _transform = new TranslateTransform(offsetX, offsetY);
        RenderTransform = _transform;
            
        base.OnPointerMoved(e);
    }
}