using System.Collections.Generic;

namespace Chatty.DAL.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }

        public virtual List<Message> Messages { get; set; }
        public virtual List<User> Users { get; set; }
    }
}