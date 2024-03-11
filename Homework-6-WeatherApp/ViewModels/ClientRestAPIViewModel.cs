using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using RestSharp;
using RestSharp.Serializers.Json;
using static System.Net.WebRequestMethods;

namespace Homework_6_WeatherApp.ViewModels
{
	public class ClientRestAPIViewModel : ReactiveObject
	{
        private const string url = "https://api.openweathermap.org/data/2.5/";
        private const string apikey = "13122507320917bdfe44143a93c7faba";

        private RestClientOptions options = new RestClientOptions(url);
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private RestClient client;
        private string _city = "";
        private string _units = "metric";
  
        public string City
        {
            get => _city;
            set => this.RaiseAndSetIfChanged(ref _city, value);
        }

        public ClientRestAPIViewModel()
        {
            client = new RestClient(
                options,
                configureSerialization: (s) => s.UseSystemTextJson(serializerOptions)
            );
        }

        public async Task<string?> GetWeatherForecastAsync(string city)
        {
            var request = new RestRequest("forecast");
            request.AddParameter("q", city);
            request.AddParameter("units", _units);
            request.AddParameter("appid", apikey);

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return response.Content;
            }
            else
            {
                return response.ErrorMessage;
            }
        }
    }
}