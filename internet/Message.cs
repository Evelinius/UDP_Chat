using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internet
{
    public class Message
    {
        public string text;
        public DateTime date;
        public bool IsMy;

        public Message(string text, bool IsMy)
        {
            this.date = DateTime.Now;
            this.text = text;
            this.IsMy = IsMy;
        }

        public override string ToString()
        {
            return (IsMy ? "Me: " : "He: ") + $"{text} {date}";
        }
    }
}
