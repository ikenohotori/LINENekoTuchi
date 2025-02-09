using LINETuchi.Entity;
using LINETuchi.Entity.Line;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LINETuchi.Api
{
    public class SendMessageAsync
    {
        private ILogger _log;
        private HttpClient _client;
        private string token = Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");

        public SendMessageAsync(ILogger log)
        {
            this._log = log;
            this._client =　HttpClientProvider.GetHttpClient();
            this._client.DefaultRequestHeaders.Authorization 
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        public async Task<SendMessageResult> SendMessage(string replyToken, List<IMessage> messages)
        {
            try
            {
                var response = await _client.PostAsJsonAsync(
                    "https://api.line.me/v2/bot/message/reply",
                    new LineTextReplyObject
                    {
                        replyToken = replyToken,
                        messages = messages.Select(m => m.CreateInstance()).ToList()
                    });
                return await CreateResult(response);
            }
            catch (Exception e)
            {
                _log.LogError(e.Message);
                return new SendMessageResult();
            }
        }
        private async Task<SendMessageResult> CreateResult(HttpResponseMessage response)
        {
            return new SendMessageResult()
            {
                HttpResponseMessage = response,
                IsSuccess = response.IsSuccessStatusCode ? true : false,
                Message = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
