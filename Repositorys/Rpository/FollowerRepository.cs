using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class FollowerRepository:IRepository<Follower>
    {
        private readonly IContext context;

        public FollowerRepository(IContext context)
        {
            this.context = context;
        }

        public Follower Add(Follower item)
        {
            context.followers.Add(item);
            context.Save();
            return item;
        }

        public void Delete(int id)
        {
            var follower = Get(id);
            if (follower != null)
            {
                context.followers.Remove(follower);
                context.Save();
            }
        }

        public Follower Get(int id)
        {
            return context.followers.FirstOrDefault(x => x.FollowerId == id);
        }

        public List<Follower> GetAll()
        {
            return context.followers.ToList();
        }
        public Follower Update(Follower item)
        {
            var existingFollower = Get(item.FollowerId);
            if (existingFollower != null)
            {
                existingFollower.UserId = item.UserId;
                existingFollower.FollowerUserId = item.FollowerUserId;
                context.Save();
            }
            return existingFollower;
        }

    }
}
