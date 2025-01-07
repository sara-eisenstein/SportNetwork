using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class AchievementRepository : Irepository<Achivevement>
    {
        private readonly IContext context;

        public AchievementRepository(IContext context)
        {
            this.context = context;
        }

        public Achivevement Add(Achivevement item)
        {
            context.achivevements.Add(item);
            context.Save();
            return item;
        }

        public void Delete(int id)
        {
            var achievement = Get(id);
            if (achievement != null)
            {
                context.achivevements.Remove(achievement);
                context.Save();
            }
        }

        public Achivevement Get(int id)
        {
            return context.achivevements.FirstOrDefault(x => x.AchievementId == id);
        }

        public List<Achivevement> GetAll()
        {
            return context.achivevements.ToList();
        }

        public Achivevement Update(Achivevement item)
        {
            var existingAchievement = Get(item.AchievementId);
            if (existingAchievement != null)
            {
                existingAchievement.UserId = item.UserId;
                existingAchievement.Title = item.Title;
                existingAchievement.Description = item.Description;
                existingAchievement.DateEarned = item.DateEarned;
                context.Save();
            }
            return existingAchievement;
        }
    }
}
