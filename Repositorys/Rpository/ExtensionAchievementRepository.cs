using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ExtensionAchievementRepository:IAchievementRepository
    {
        private readonly IContext context;

        public ExtensionAchievementRepository(IContext context)
        {
            this.context = context;
        }

        public List<Achievement> GetAchievementsByUserId(int userId)
        {
            return context.achivevements.Where(x => x.UserId == userId).ToList();

        }
    }
}
