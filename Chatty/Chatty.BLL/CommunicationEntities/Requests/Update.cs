using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatty.BLL.Contracts;

namespace Chatty.BLL.CommunicationEntities.Requests
{
    public class Update : Request
    {
        public override CommunicationObject Handle(ICommunicationManager comManager)
        {
            throw new NotImplementedException();
        }
    }
}
