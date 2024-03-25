using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text.Json;
using DynamicData;
using System.Threading.Tasks;
using ReactiveUI;
using RestSharp;
using RestSharp.Serializers.Json;
using Homework_8_MVVM.Models;

namespace Homework_8_MVVM.ViewModels
{
	public class HttpClientViewModel : ReactiveObject
	{
        private const string url = "https://jsonplaceholder.typicode.com/";
        private RestClientOptions options = new RestClientOptions(url);
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        private RestClient client;

        public HttpClientViewModel()
        {
            this.client = new RestClient(
                options,
                configureSerialization: (s) => s.UseSystemTextJson(serializerOptions)
            );
        }

        public async Task<List<Users>?> GetWebDataUsers()
        {
            var response = await client.GetJsonAsync<List<Users>>("users");

            return response;
        }

    }
}