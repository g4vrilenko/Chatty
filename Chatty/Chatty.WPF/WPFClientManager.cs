using Chatty.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatty.BLL.CommunicationEntities;
using Chatty.BLL.Entities;
using System.Windows.Threading;

namespace Chatty.WPF
{
    public class WPFClientManager : IClientManager
    {
        private IClientManager _clientManager;
        private Dispatcher _dispetcher;

        public WPFClientManager(IClientManager _clientManager, Dispatcher _dispetcher)
        {
            this._clientManager = _clientManager;
            this._dispetcher = _dispetcher;
        }

        public bool ConnecToServer(string ipAddress, int port)
        {
            return _clientManager.ConnecToServer(ipAddress, port);
        }

        public void SendMessage(Message msg, Action<Response> callback)
        {
            _clientManager.SendMessage(msg, (r) => { _dispetcher.Invoke(() => callback(r)); });
        }
    }
}
