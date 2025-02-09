using LINETuchi.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LINETuchi.Api
{
    public class Unsplash
    {
        private ILogger _log;
        private HttpClient _client;
        private Random _random;
        public Unsplash(ILogger log)
        {
            this._log = log;
            this._random = new Random();
            this._client = HttpClientProvider.GetHttpClient();
            this._client.DefaultRequestHeaders.Add("Authorization", "Client-ID " + Environment.GetEnvironmentVariable("Unsplash_ID"));
        }
        public async Task<UnsplashResult> GetRandomCatPhoto(string emotion)
        {
            try
            {
                var response = await this._client.
                    GetAsync($"https://api.unsplash.com/search/photos?query=cat+{emotion}&per_page=30&page={_random.Next(1,3)}");

                _log.LogInformation(response.ToString());
                _log.LogInformation(response.Content.ReadAsStringAsync().Result);
                return await CreateUnsplashResult(response);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }

            return new UnsplashResult();
        }

        public async Task<UnsplashResult> CreateUnsplashResult(HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonDocument.Parse(content).RootElement.GetProperty("results")[this._random.Next(0,29)];

            return new UnsplashResult()
            {
                HttpResponseMessage = httpResponseMessage,
                IsSuccess = httpResponseMessage.IsSuccessStatusCode ? true : false,
                Url = httpResponseMessage.IsSuccessStatusCode ? 
                    result.GetProperty("urls").GetProperty("regular").GetString() 
                    : null
            };
        }
    }
}
