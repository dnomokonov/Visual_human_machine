using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace Homework_LogicalApp.Controls;

public class Connector : Control
{ 
    //private List<Point> Points { get; set; }
    
    // Получаю первоначальные координаты спавна
    // Цель: получение динамических координат
    public void Connect(Connector? obj)
    {
        if (obj == null) return;

        var posX = obj.Bounds.X;
        var posY = obj.Bounds.Y;

        var point = new Point(posX, posY);

        Console.WriteLine($"x = {posX} | Y = {posY}");
    }

    public void Draw(DrawingContext context)
    {
        var renderSize = Bounds.Size;
        
        var brush = Brushes.Blue;
        var pen = new Pen(brush, 1);
        var rect = new Rect(0, 0, renderSize.Width, renderSize.Height);
        context.DrawRectangle(brush, pen, rect);
    }
}