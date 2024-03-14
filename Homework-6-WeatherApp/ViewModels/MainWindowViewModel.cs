using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace Homework_6_WeatherApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string? _city = "";
    private List<ShowContentViewModel.WeatherVariable>? weather = new List<ShowContentViewModel.WeatherVariable>();
    public DateViewModel dateViewModel { get; }
    public ClientRestAPIViewModel ClientRestAPI { get; }
    public SwapThemeViewModel swapThemeViewModel { get; }
    public ReactiveCommand<Unit, Unit> SearchCommand { get; }

    public MainWindowViewModel()
    {
        dateViewModel = new DateViewModel();
        ClientRestAPI = new ClientRestAPIViewModel();
        swapThemeViewModel = new SwapThemeViewModel();

        SearchCommand = ReactiveCommand.CreateFromTask(ExecuteSearch);

        Task.Run(async () =>
        {
            await UpdateWeather();
        });
    }

    public string? City
    {
        get => _city;
        set => this.RaiseAndSetIfChanged(ref _city, value);
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

        string? d = weather[0].IconCode;
        swapThemeViewModel.SwapTheme(d);

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

    public async Task ExecuteSearch()
    {
        await UpdateWeather();
    }

    public async Task UpdateWeather()
    {
        string? json = await ClientRestAPI.GetWeatherForecastAsync(City);
        if (json != null)
        {
            weather = ShowContentViewModel.ParseWeatherData(json);
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
