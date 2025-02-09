using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Api
{
    public static class HttpClientProvider
    {
        // これを使いまわす
        private static HttpClient httpClient = new HttpClient();

        public static HttpClient GetHttpClient()
        {
            httpClient.DefaultRequestHeaders.Clear();
            return httpClient;
        }
    }
}
