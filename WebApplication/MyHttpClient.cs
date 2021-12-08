using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Laba1.Models;
using Laba1.Services;
using Laba2;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication
{
    public class MyHttpClient
    {
            private const string DefaultHostUrl = "https://tolltech.ru/study/";
            private readonly HttpClient _client = new();
            private readonly string _hostUrl;
            private Serializer _serializer;

            public MyHttpClient(string hostUrl)
            {
                _hostUrl = hostUrl ?? DefaultHostUrl;
                _serializer = new Serializer();
            }

            public async Task<HttpResponseMessage> Ping()
            {
                return await _client.GetAsync("ping");
            }
            
            public void GetInputData(string inputString)
            {
                _client.PostAsync("/PostInputData", new StringContent(inputString, Encoding.UTF8, "application/json"));
            }

            public Task<string> WriteAnswer()
            {
                return _client.GetStringAsync("/GetAnswer");
            }
            
            public async Task<HttpResponseMessage> Stop()
            {
                return await _client.GetAsync("Stop");
            }
    }
}
