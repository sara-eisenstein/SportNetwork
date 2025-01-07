using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ChallengeRepository:IRepository<Challenge>
    {
        private readonly IContext context;

        public ChallengeRepository(IContext context)
        {
            this.context = context;
        }
        public Challenge Add(Challenge item)
        {
            context.challenges.Add(item);
            context.Save();
            return item;
        }

        public void Delete(int id)
        {
            var challenge = Get(id);
            if (challenge != null)
            {
                context.challenges.Remove(challenge);
                context.Save();
            }
        }

        public Challenge Get(int id)
        {
            return context.challenges.FirstOrDefault(x => x.ChallengeId == id);
        }

        public List<Challenge> GetAll()
        {
            return context.challenges.ToList();
        }

        public Challenge Update(Challenge item)
        {
            var existingChallenge = Get(item.ChallengeId);
            if (existingChallenge != null)
            {
                existingChallenge.Title = item.Title;
                existingChallenge.Description = item.Description;
                existingChallenge.StartDate = item.StartDate;
                existingChallenge.EndDate = item.EndDate;
                context.Save();
            }
            return existingChallenge;
        }

    }
}
