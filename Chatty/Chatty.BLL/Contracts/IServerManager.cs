using Chatty.BLL.Entities;

namespace Chatty.BLL.Contracts
{
    public interface IServerManager : ICommunicationManager
    {
        void BroadcastMessage(Message msg);
    }
}