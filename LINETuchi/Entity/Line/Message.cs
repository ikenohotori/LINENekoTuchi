using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Entity.Line
{
    public interface IMessage
    {
        public string type { get; set; }

        public IMessage CreateInstance();
    }
    public class MessageText : IMessage
    {
        public string text { get; set; }
        public string type { get; set; }
        public quickReply quickReply { get; set; }

        public MessageText()
        {
        }
        public MessageText(string message, quickReply quickReply)
        {
            type = "text";
            text = message;
            this.quickReply = quickReply;
        }

        public IMessage CreateInstance()
        {
            return new MessageText()
            {
                type = this.type,
                text = this.text,
                quickReply = this.quickReply
            };
        }
    }

    public class MessageImage : IMessage
    {
        public string originalContentUrl { get; set; }
        public string previewImageUrl { get; set; }
        public string type { get; set; }
        public MessageImage()
        {
        }
        public MessageImage(string url)
        {
            type = "image";
            originalContentUrl = url;
            previewImageUrl = url;
        }

        public IMessage CreateInstance()
        {
            return new MessageImage()
            {
                type = this.type,
                originalContentUrl = this.originalContentUrl,
                previewImageUrl = this.previewImageUrl
            };
        }
    }

}
