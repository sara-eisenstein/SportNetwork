using Common.Dto;
using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Interface
{
    public interface IChallengeRepository
    {
        List<Challenge> GetChallengesByUserId(int userId);
        List<User> GetChallengePrticipantsById(int challengeId);
    }
}
