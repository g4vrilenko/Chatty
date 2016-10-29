using System.Collections.Generic;

namespace Chatty.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual List<Message> Messages { get; set; }
        public virtual List<Room> Rooms { get; set; }
    }
}
