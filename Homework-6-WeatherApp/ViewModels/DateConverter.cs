using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Homework_6_WeatherApp.Converters
{
	public class DateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("dddd, d", new CultureInfo("en-US"));
            }
            return value;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}