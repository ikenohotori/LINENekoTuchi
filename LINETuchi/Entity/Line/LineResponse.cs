using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Entity.Line
{
    public class LineResponse
    {
        public string Destination { get; set; }
        public List<Event> Events { get; set; }
    }
    public class Event
    {
        public string ReplyToken { get; set; }
        public string Type { get; set; }
        public object Timestamp { get; set; }
        public Source Source { get; set; }
        public MessageText Message { get; set; }
    }

    public class Source
    {
        public string Type { get; set; }
        public string UserId { get; set; }
    }
}
