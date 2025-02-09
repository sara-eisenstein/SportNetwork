using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class AchievementRepository : IRepository<Achievement>
    {
        private readonly IContext context;

        public AchievementRepository(IContext context)
        {
            this.context = context;
        }

        public Achievement Add(Achievement item)
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

        public Achievement Get(int id)
        {
            return context.achivevements.FirstOrDefault(x => x.AchievementId == id);
        }

        public List<Achievement> GetAll()
        {
            return context.achivevements.ToList();
        }

        public Achievement Update(Achievement item, int id)
        {
            var existingAchievement = Get(id);

            if (existingAchievement != null)
            {
                existingAchievement.UserId = item.UserId;
                existingAchievement.Title = item.Title;
                existingAchievement.Description = item.Description;
                existingAchievement.DateEarned = item.DateEarned;

                context.achivevements.Update(existingAchievement);


                context.Save();
            }
            return existingAchievement;
        }
    }
}
