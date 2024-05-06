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
    
    // Получаю первоначальные координаты / нет новых
    public void Connect(Connector? obj)
    {
        if (obj == null) return;

        var posX = obj.Bounds.X;
        var posY = obj.Bounds.Y;

        var point = new Point(posX, posY);
        
        Console.WriteLine($"x = {posX} | Y = {posY}");
    }
    
}