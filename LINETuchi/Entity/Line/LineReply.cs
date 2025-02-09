using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Entity.Line
{
    public class LineTextReplyObject
    {
        public string replyToken { get; set; }
        public List<IMessage> messages { get; set; }
    }
}
