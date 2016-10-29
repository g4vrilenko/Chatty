using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatty.BLL.Contracts;

namespace Chatty.BLL.CommunicationEntities
{
    [Serializable]
    public class Response : CommunicationObject
    {
        public ResponseStatus Status { get; set; }
        public string ErrorMessage{ get; set; }

        public override CommunicationObject Handle(ICommunicationManager comManager)
        {
            throw new NotImplementedException();
        }
    }
}
