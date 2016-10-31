using Chatty.DAL.Models;
using System.Data.Entity;

namespace Chatty.DAL
{
    public class ChattyContext : DbContext
    {
        public ChattyContext() : base("Chatty") 
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
