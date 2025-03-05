using Common.Dto;
using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ExtensionUserRepository:IUserRepository
    {
        private readonly IContext context;
        public ExtensionUserRepository(IContext context)
        {
            this.context = context;
        }

        public User GetUserByEmail(string email)
        {
            return context.users.FirstOrDefault(p => p.Email == email);
        }
    }
}
