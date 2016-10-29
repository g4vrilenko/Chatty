using System;

namespace Chatty.DAL.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime Time{ get; set; }

        public virtual User User { get; set; }
        public virtual Room Room{ get; set; }
    }
}