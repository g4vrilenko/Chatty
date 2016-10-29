using Chatty.BLL.Contracts;
using System;

namespace Chatty.BLL.CommunicationEntities
{
    [Serializable]
    public abstract class CommunicationObject
    {
        public abstract CommunicationObject Handle(ICommunicationManager comManager);
    }
}
