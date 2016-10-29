using Chatty.BLL.Contracts;
using Chatty.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.BLL.Network
{
    public class ServerManager : IServerManager
    {
        public event Action OnServerStart;


        public void BroadcastMessage(Message msg)
        {
            throw new NotImplementedException();
        }

        public async void StartServer(string ipAddress, int port)
        {            
            var server = new Server(ipAddress, port, this);
            server.ServerEvent += Server_ServerStarted;
            await server.RunAsync();
        }

        private void Server_ServerStarted(string str)
        {
            OnServerStart?.Invoke();
        }
    }
}
