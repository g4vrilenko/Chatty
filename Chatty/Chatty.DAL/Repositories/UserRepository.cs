﻿using Chatty.DAL.Contracts;
using Chatty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Chatty.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChattyContext context) : base(context)
        {
        }
    }
}
