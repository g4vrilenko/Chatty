using Chatty.DAL;
using Chatty.DAL.Models;
using Chatty.DAL.Repositories;
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

        [TestMethod]
        public void AddMessage_Test()
        {
            //using (var uow = new UnitOfWork(new ChattyContext()))
            //{
            //    var user = uow.Users.Get(1);
            //    uow.Rooms.Add(new Room() {
            //        Name = "First Room"                                        
            //    });
            //    var room = uow.Rooms.Get(1);
            //    var messge = new Message()
            //    {
            //        Text = "Hello",
            //        Time = DateTime.Now,
            //        User = user,
            //        Room = room                    
            //    };
            //    uow.Messages.Add(messge);
            //    uow.Complete();
            //}
            
        }
    }
}
