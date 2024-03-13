using System;
using System.Collections.Generic;
using Avalonia.Media.Imaging;
using System.Text.Json;
using System.Xml.Linq;
using Avalonia.Platform;
using ReactiveUI;

namespace Homework_6_WeatherApp.ViewModels
{
	public class ShowContentViewModel : ReactiveObject
	{
        public class WeatherVariable
        {
            public DateTime? Date { get; set; }
            public string? TempMin { get; set; }
            public string? TempMax { get; set; }
            public string? TempFeel { get; set; }
            public string? Pressure { get; set; }
            public string? Humidity { get; set; }
            public string? WindSpeed { get; set; }
            public double? Pop { get; set; }
            public string? IconCode {  get; set; }
            public Bitmap? Icon { get; set; }
        }

        public static List<WeatherVariable> ParseWeatherData(string json)
        {
            List<WeatherVariable> weatherForecast = new List<WeatherVariable>();
            try
            {
                JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;
                JsonElement list = root.GetProperty("list");

                foreach (var elem in list.EnumerateArray())
                {
                    WeatherVariable weatherVariable = new WeatherVariable
                    {
                        Date = DateTimeOffset.FromUnixTimeSeconds(elem.GetProperty("dt").GetInt64()).DateTime,
                        TempMin = (Math.Floor(elem.GetProperty("main").GetProperty("temp_min").GetDouble())).ToString() + "°",
                        TempMax = (Math.Ceiling(elem.GetProperty("main").GetProperty("temp_max").GetDouble())).ToString() + "°",
                        TempFeel = (Math.Ceiling(elem.GetProperty("main").GetProperty("feels_like").GetDouble())).ToString() + "°",
                        Pressure = (elem.GetProperty("main").GetProperty("pressure").GetInt32()).ToString() + " hPa",
                        Humidity = (elem.GetProperty("main").GetProperty("humidity").GetInt32()).ToString() + "%",
                        WindSpeed = (elem.GetProperty("wind").GetProperty("speed").GetDouble()).ToString() + " ms",
                        Pop = elem.GetProperty("pop").GetDouble(),
                    };

                    string? iconCode = elem.GetProperty("weather")[0].GetProperty("icon").GetString();
                    string path = $"avares://Homework-6-WeatherApp/Assets/{iconCode}.png";
                    Bitmap icon = new Bitmap(AssetLoader.Open(new Uri(path)));

                    weatherVariable.IconCode = iconCode;
                    weatherVariable.Icon = icon;

                    weatherForecast.Add(weatherVariable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return weatherForecast;
        }
    }
}