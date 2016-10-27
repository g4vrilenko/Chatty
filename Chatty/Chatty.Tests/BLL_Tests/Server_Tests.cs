using Chatty.DAL.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Tests.BLL_Tests
{
    [TestClass]
    public class Server_Tests
    {
        [TestMethod]
        public void Constructor_IpAdreaaAndPort_InstanceOfServerClass()
        {
            var server = new Server("127.0.0.1", 8080);
            
        }
    }
}
