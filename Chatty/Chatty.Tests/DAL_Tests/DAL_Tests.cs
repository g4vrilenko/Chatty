using Chatty.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty.Tests.DAL_Tests
{
    [TestClass]
    public class DAL_Tests
    {
        [TestMethod]
        public void ChattyContext_Test()
        {
            using (var context = new ChattyContext())
            {
                var user = new User()
                {
                    Username = "Test",
                    Password = "123"
                };
                var userExists = context.Users
                    .Where(x => x.Username == user.Username)
                    .Count() > 0;
                if (!userExists)
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    userExists = context.Users
                    .Where(x => x.Username == user.Username)
                    .Count() > 0;
                }                
                Assert.IsTrue(userExists);
            }
        }
    }
}
