using System;
using System.Collections.Generic;
using System.Text.Json;
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
            //public Bitmap? Icon { get; set; }
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
                        //Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://Homework_6_ExternalServices/Assets/{elem.GetProperty("weather").GetProperty("icon").GetString()}.png")))
                    };

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