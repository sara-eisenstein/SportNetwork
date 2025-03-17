using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Interface
{
    public interface IFollowerRepository
    {
        List<User> GetFollowersByUserId(int userId);
        List<User> GetFollowingByUserId(int userId);

    }
}
