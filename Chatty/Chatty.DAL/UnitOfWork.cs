using Chatty.DAL.Contracts;
using Chatty.DAL.Repositories;

namespace Chatty.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChattyContext _context;

        public UnitOfWork(ChattyContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Rooms = new RoomRepository(_context);
            Messages = new MessageRepository(_context);
        }

        public IUserRepository Users { get; set; }
        public IRoomRepository Rooms { get; set; }
        public IMessageRepository Messages { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
