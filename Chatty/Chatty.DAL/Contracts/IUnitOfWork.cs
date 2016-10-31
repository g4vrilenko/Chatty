using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; set; }
        IRoomRepository Rooms { get; set; }
        IMessageRepository Messages { get; set; }

        int Complete();
    }
}
