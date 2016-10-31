using Chatty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Chatty.DAL.Contracts;

namespace Chatty.DAL.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public ChattyContext ChattyContext
        {
            get
            {
                return Context as ChattyContext;
            }
        }
        public RoomRepository(ChattyContext context) : base(context)
        {
        }

        public IEnumerable<Message> GetTopMessages(int roomId, int count)
        {
            return ChattyContext.Rooms
                .Where(r => r.RoomId == roomId)
                .Select(r => r.Messages)
                .FirstOrDefault()
                .OrderByDescending(m => m.Time)
                .Take(count)
                .ToList();
        }
    }
}
