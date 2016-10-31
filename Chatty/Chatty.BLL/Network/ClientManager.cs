using Chatty.BLL.CommunicationEntities;
using Chatty.BLL.Contracts;
using Chatty.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatty.BLL.Network
{
    public class ClientManager : IClientManager
    {
        private IClient _client;

        public ClientManager()
        {
            
        }

        public bool ConnecToServer(string ipAddress, int port)
        {
            _client = new Client(ipAddress, port);
            return _client.Connect();
        }

        public void SendMessage(Message msg, Action<Response> callback)
        {
            var req = new SendNewMessage(msg);
            _client.AddRequest(req, callback);
        }
    }
}
