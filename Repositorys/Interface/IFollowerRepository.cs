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
        List<Follower> GetFollowersByUserId(int userId);
    }
}
