using System;
using System.Linq;
using System.Net.Http;
using Laba1.Services;

namespace Laba2
{
    public abstract class HttpClientBase
    {
        private readonly string _hostUrl;
        private readonly Serializer _serializer = new();
        private readonly HttpClient _client = new();

        protected HttpClientBase(string hostUrl)
        {
            _hostUrl = hostUrl;
        }

        protected T MakeGetRequest<T>(string methodName, params (string Key, string Value)[] parameters)
        {
            var urlParameters = GetUrlParameters(parameters);
            var response = _client.GetAsync($"{_hostUrl}{methodName}?{urlParameters}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            return _serializer.DeserializeJsonToModel<T>(responseBody);
        }

        protected void MakePostRequest<TRequest>(string methodName, TRequest requestBody,
            params (string Key, string Value)[] parameters)
        {
            var urlParameters = GetUrlParameters(parameters);
            var requestStringContent = GetRequestStringContent(requestBody);

            var response = _client.PostAsync($"{_hostUrl}{methodName}?{urlParameters}", requestStringContent).Result;
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception($"Http status code {response.StatusCode}. Сообщение. {response.ReasonPhrase}");
            }

            response.Content.ReadAsStringAsync().Wait();
        }

        private StringContent GetRequestStringContent<T>(T requestModel)
        {
            var requestBody = _serializer.SerializeToJson(requestModel);
            return new StringContent(requestBody);
        }

        private string GetUrlParameters(params (string Key, string Value)[] parameters) =>
            string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));
    }
}
