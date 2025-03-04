using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ExtensionFollowerRepository : IFollowerRepository
    {
        private readonly IContext context;

        public ExtensionFollowerRepository(IContext context)
        {
            this.context = context;
        }
        public List<Follower> GetFollowersByUserId(int userId)
        {
            
            
                return context.followers.Where(x => x.UserId == userId).ToList();
            
        }
    }
}
