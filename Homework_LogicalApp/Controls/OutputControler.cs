using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Input;
using System.Collections.Generic;
using Avalonia.Controls.Shapes;

namespace Homework_LogicalApp.Controls;

public class OutputControler : Control
{
    public int CountInput { get; set; }
    public bool BoolArray { get; set; }
    
    public IBrush? Stroke { get; set; }
    public double StrokeThickness { get; set; }
    public string SetFonts { get; set; }
    
    private bool _isSelected;
    private const double Radius = 5;

    public OutputControler()
    {
        Width = 60;
        Height = 30;
        Stroke = Brushes.Black;
        StrokeThickness = 2;
        SetFonts = "Arial";
        _isSelected = false;
    }

    public sealed override void Render(DrawingContext context)
    {
        var renderSize = Bounds.Size;
        
        // Set Fonts for text
        var typeface = new Typeface(SetFonts);

        // Set outline color based on selection
        var outlineBrush = _isSelected ? Brushes.OrangeRed : Brushes.Black;
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
    }
    
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        
        var point = e.GetPosition(this);
        if (!Bounds.Contains(point)) return;
        _isSelected = !_isSelected;
        InvalidateVisual();
    }
}