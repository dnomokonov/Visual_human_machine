using System;
using System.Collections.Generic;
using Avalonia.Platform;
using ReactiveUI;
using Avalonia.Media.Imaging;

namespace Homework_6_WeatherApp.ViewModels
{
	public class IconHelperViewModel : ReactiveObject
	{
        public static Bitmap IconNameToBitmap(string imageName)
        {
            var path = $"avares://Homework_6_WeatherApp/Assets/" + imageName + ".png";
            Bitmap bmp = new Bitmap(AssetLoader.Open(new Uri(path)));
            return bmp;
        }
    }
}