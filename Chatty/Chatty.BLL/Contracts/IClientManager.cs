using Chatty.BLL.CommunicationEntities;
using Chatty.BLL.Entities;
using System;

namespace Chatty.BLL.Contracts
{
    public interface IClientManager : ICommunicationManager
    {
        bool ConnecToServer(string ipAddress, int port);
        void SendMessage(Message msg, Action<Response> callback);
    }
}
