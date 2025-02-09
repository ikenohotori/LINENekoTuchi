using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Entity.Line
{
    public class quickReply
    {
        public List<item> items { get; set; }
    }
    public class item
    {
        public string type { get; set; }
        public string imageurl { get; set; }
        public action action { get; set; }
    }

    public class action
    {
        public string type { get; set; }
        public string label { get; set; }
        public string text { get; set; }
    }
}
