using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ChallengeParticipantRepository : IRepository<ChallengeParticipant>
    {
        private readonly IContext context;

        public ChallengeParticipantRepository(IContext context)
        {
            this.context = context;
        }

        public ChallengeParticipant Add(ChallengeParticipant item)
        {
            context.challengeParticipants.Add(item);
            context.Save();
            return item;
        }

        public void Delete(int id)
        {
            var participant = Get(id);
            if (participant != null)
            {
                context.challengeParticipants.Remove(participant);
                context.Save();
            }
        }

        public ChallengeParticipant Get(int id)
        {
            return context.challengeParticipants.FirstOrDefault(x => x.ChallengeParticipantId == id);
        }

        public List<ChallengeParticipant> GetAll()
        {
            return context.challengeParticipants.ToList();
        }

        public ChallengeParticipant Update(ChallengeParticipant item,int id)

        {
            var existingParticipant = Get(id);
            if (existingParticipant != null)
            {
                existingParticipant.ChallengeId = item.ChallengeId;
                existingParticipant.UserId = item.UserId;
                existingParticipant.Progress = item.Progress;
                context.challengeParticipants.Update(existingParticipant);


                context.Save();
            }
            return existingParticipant;
        }


    }
}
