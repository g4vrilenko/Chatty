using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatty.BLL.Contracts;
using Chatty.BLL.Entities;

namespace Chatty.BLL.CommunicationEntities
{
    [Serializable]
    public class NewMessage : Request
    {
        public NewMessage(Message message)
        {
            Message = message;
        }

        public Message Message { get; set; }

        public override CommunicationObject Handle(ICommunicationManager comManager)
        {
            var serverMeneger = comManager as IServerManager;
            serverMeneger.BroadcastMessage(Message);
            return new Response() { Status = ResponseStatus.Ok };
        }
    }
}
