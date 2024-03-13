using System;
using System.Collections.Generic;
using Avalonia.Media;
using ReactiveUI;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Security.Cryptography.X509Certificates;

namespace Homework_6_WeatherApp.ViewModels
{
	public class SwapThemeViewModel : ReactiveObject
    {
		private string colorLight = "#2492EE";
		private string colorDark = "#052D75";
        private IBrush? _backgroundBrush;

         public SwapThemeViewModel()
        {
            SwapTheme("");
        }

        public IBrush? BackgroundBrush
        {
            get => _backgroundBrush;
            set => this.RaiseAndSetIfChanged(ref _backgroundBrush, value);
        }

        public void SwapTheme(string? type) {
			if (type != null)
			{
                char timeDayCode = ' ';
                if (type.Length == 3)
                {
                    timeDayCode = type[2];
                }

                if (timeDayCode == 'd')
                {
                    BackgroundBrush = new SolidColorBrush(Color.Parse(colorLight));
                }
                else if (timeDayCode == 'n')
                {
                    BackgroundBrush = new SolidColorBrush(Color.Parse(colorDark));
                }
                else
                {
                    BackgroundBrush = new SolidColorBrush(Color.Parse(colorLight));
                }
            }
            else
            {
                BackgroundBrush = new SolidColorBrush(Color.Parse(colorLight));
            }
		}
	}
}