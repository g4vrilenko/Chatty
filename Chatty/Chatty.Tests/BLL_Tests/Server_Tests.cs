using Chatty.BLL.CommunicationEntities;
using Chatty.BLL.Helpers;
using Chatty.BLL.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Chatty.Tests.BLL_Tests
{
    [TestClass]
    public class Server_Tests
    {
        private bool _serverStarted;

        [TestMethod]
        public void StartServer_IpAdreaaAndPort_RunServer()
        {
            var sm = new ServerManager();
            sm.OnServerStart += Sm_OnServerStart;
            sm.StartServer("127.0.0.1", 51111);    
            Thread.Sleep(500);
            Assert.IsTrue(_serverStarted);       
        }

        private void Sm_OnServerStart()
        {
            _serverStarted = true;
        }

        [TestMethod]
        public void SerializeAadDeserialize_Null_Null()
        {
            Assert.IsNull(CommunicationHelper.Serialize(null));
            Assert.IsNull(CommunicationHelper.Deserialize(null));
        }

        [TestMethod]
        public void SendMessage_Message_ResponseStatusOk()
        {
            var ip = "127.0.0.1";
            var port = 8080;
            var serverManager = new ServerManager();
            var clientManger = new ClientManager();
            serverManager.StartServer(ip, port);
            clientManger.ConnecToServer(ip, port);
            ResponseStatus status = ResponseStatus.Error; 
            clientManger.SendMessage(new BLL.Entities.Message(1, 1, "Hello"), (res) =>
            {
                status = res.Status;
            });
            Thread.Sleep(300);
            Assert.IsTrue(status == ResponseStatus.Ok);
        }
    }
}
