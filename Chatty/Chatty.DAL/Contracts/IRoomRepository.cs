using Chatty.DAL.Models;
using System.Collections.Generic;

namespace Chatty.DAL.Contracts
{
    public interface IRoomRepository : IRepository<Room>
    {
        IEnumerable<Message> GetTopMessages(int roomId, int count);
    }
}
