using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatty.BLL.CommunicationEntities;

namespace Chatty.BLL.Contracts
{
    public interface IClient
    {
        bool Connect();
        void AddRequest(Request req, Action<Response> callback);
    }
}
