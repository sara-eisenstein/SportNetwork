using Microsoft.EntityFrameworkCore;
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

        public List<User> GetFollowersByUserId(int userId)
        {
            return context.followers
                .Where(f => f.FollowerUserId == userId) // מחפש את כל המשתמשים שעוקבים אחרי המשתמש הזה
                .Select(f => f.User) // ✅ מחזיר את רשימת *העוקבים* (מי שעוקב אחרי המשתמש)
                .ToList();
        }


        public List<User> GetFollowingByUserId(int userId)
        {
            return context.followers
                .Where(f => f.UserId == userId) // מחפש את כל המשתמשים שהמשתמש הזה עוקב אחריהם
                .Select(f => f.FollowerUser) // ✅ מחזיר את רשימת *הנעקבים* (מי שהמשתמש עוקב אחריו)
                .ToList();
        }

       public bool UnfollowUser(int userId, int unfollowUserId)
        {
            var followEntry = context.followers
        .FirstOrDefault(f => f.UserId == userId && f.FollowerUserId == unfollowUserId);

            if (followEntry == null)
            {
                return false; // המעקב לא נמצא, אין מה להסיר
            }

            context.followers.Remove(followEntry);
            context.Save();
            return true; // המעקב הוסר בהצלחה
        }


    }
}
