using System;
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
            return new Response() { Status = ResponseStatus.Ok };
        }
    }
}
