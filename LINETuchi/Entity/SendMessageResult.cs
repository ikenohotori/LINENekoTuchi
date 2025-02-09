using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Entity
{
    public class SendMessageResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public SendMessageResult()
        {
            IsSuccess = false;
            Message = "";
            HttpResponseMessage = null;
        }
    }
}
