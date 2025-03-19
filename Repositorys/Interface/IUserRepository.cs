using Common.Dto;
using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Interface
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        public string getUserName(int userId);

    }
}
