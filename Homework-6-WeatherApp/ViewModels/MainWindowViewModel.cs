using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Globalization;

namespace Homework_6_WeatherApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string? _city = "Moscow";
    private List<ShowContentViewModel.WeatherVariable>? weather = new List<ShowContentViewModel.WeatherVariable>();
    public DateViewModel dateViewModel { get; }
    public ClientRestAPIViewModel ClientRestAPI { get; }

    public MainWindowViewModel()
    {
        dateViewModel = new DateViewModel();
        ClientRestAPI = new ClientRestAPIViewModel();

        Task.Run(async () =>
        {
            await UpdateWeather();
        });
    }

    public string? City
    {
        get => _city;
        set => _city = value;
    }

    public List<ShowContentViewModel.WeatherVariable>? Weathers
    {
        get { return GetTopFiveWeatherVariables(); }
        set { this.RaiseAndSetIfChanged(ref weather, value); }
    }

    public List<ShowContentViewModel.WeatherVariable>? WeatherDay
    {
        get { return GetWeatherDay(); }
        set { this.RaiseAndSetIfChanged(ref weather, value); }
    }

    private List<ShowContentViewModel.WeatherVariable>? GetTopFiveWeatherVariables()
    {
        if (weather == null || weather.Count == 0)
        {
            return null;
        }

        return weather.Take(Math.Min(5, weather.Count)).ToList();
    }

    private List<ShowContentViewModel.WeatherVariable>? GetWeatherDay()
    {
        if (weather == null || weather.Count == 0)
        {
            return null;
        }

        var groupedDate = weather.GroupBy(w => w.Date.Value.Day);
        var uDate = groupedDate.Select(g => g.First()).ToList();

        return uDate;
    }

    public async Task UpdateWeather()
    {
        string? json = await ClientRestAPI.GetWeatherForecastAsync(City);
        if (json != null)
        {
            Weathers = ShowContentViewModel.ParseWeatherData(json);
            WeatherDay = ShowContentViewModel.ParseWeatherData(json);
        }
        else
        {
            Weathers = ShowContentViewModel.ParseWeatherData("");
            WeatherDay = ShowContentViewModel.ParseWeatherData("");
        }
    }
}
