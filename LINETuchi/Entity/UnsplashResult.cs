using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Entity
{
    public class UnsplashResult
    {
        public bool IsSuccess { get; set; }
        public string Url { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public UnsplashResult()
        {
            IsSuccess = false;
            Url = "";
            HttpResponseMessage = null;
        }
    }
}
