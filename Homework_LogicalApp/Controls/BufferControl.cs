using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace Homework_LogicalApp.Controls;

public class BufferControl : Control
{
    private const double Radius = 4;
    private bool _isSelected;
    private bool _isPressed; 
    private Point _positionInBlock;
    private TranslateTransform _transform = null!;
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public string SetFonts { get; set; }
    public int CountInput { get; set; } // count of inputs 
    public int SizeHeader { get; set; }
    public string HeaderValve { get; set; }
    public int SizeLabel { get; set; }
    public string LabelValve { get; set; }
    public string TypeValve { get; set; } // Type: GOST or ANSI 

    public BufferControl()
    {
        Width = 50;
        Height = 100;
        Stroke = Brushes.Black;
        StrokeThickness = 2;
        SizeHeader = 20;
        SizeLabel = 18;
        HeaderValve = "1";
        LabelValve = "Buffer";
        SetFonts = "Arial";
        TypeValve = "GOST";
        CountInput = 1;
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
            
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(0, sideLength),
                new Point(centerX + sideLength / 2, centerY)
            };
            
            // Draw triangle-box
            context.DrawLine(outlinePen, point1, point2);
            context.DrawLine(outlinePen, point1, point3);
            context.DrawLine(outlinePen, point2, point3);

            // Draw Circle-output
            context.DrawEllipse(Brushes.Red, outlinePen, new Rect(centerX + sideLength / 2 - Radius, centerY - Radius, Radius * 2, Radius * 2));

            // Draw Circle-input
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Blue, outlinePen, new Rect(x1 - Radius, y1 - Radius, Radius * 2, Radius * 2));

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
                lsize <= 4 ? new Point(lsize, posLabelY) : new Point(posLabelX - lsize + 5, posLabelY));

            // Draw left circle-input
            var x2 = renderSize.Width;
            var y2 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Red, outlinePen, new Rect(x2 - Radius, y2 - Radius, Radius * 2, Radius * 2));

            // Draw Circle-input
            var x1 = 0;
            var y1 = renderSize.Height / 2;
            context.DrawEllipse(Brushes.Blue, outlinePen, new Rect(x1 - Radius, y1 - Radius, Radius * 2, Radius * 2));
        }

        base.Render(context);
    }
    
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.PointerUpdateKind != PointerUpdateKind.LeftButtonPressed) return;
        _isSelected = !_isSelected;
        InvalidateVisual();
        
        e.Handled = true;
        _isPressed = true;
        _positionInBlock = e.GetPosition(Parent as Visual);
            
        if (_transform != null!) 
            _positionInBlock = new Point(
                _positionInBlock.X - _transform.X,
                _positionInBlock.Y - _transform.Y);
        
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