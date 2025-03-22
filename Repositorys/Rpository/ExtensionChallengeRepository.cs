using Common.Dto;
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
    internal class ExtensionChallengeRepository:IChallengeRepository
    {
        private readonly IContext context;
        public ExtensionChallengeRepository(IContext context)
        {
            this.context = context;
        }

        public List<Challenge> GetChallengesByUserId(int userId)
        {
            return context.challenges
                .Where(c => c.Participants.Any(p => p.UserId == userId))
                .ToList();
        }

        public List<User> GetChallengePrticipantsById(int challengeId)
        {
            return context.challengeParticipants
            .Where(cp => cp.ChallengeId == challengeId)
            .Include(cp => cp.User)
            .Select(cp => cp.User)
            .ToList();
        }


    }
}
