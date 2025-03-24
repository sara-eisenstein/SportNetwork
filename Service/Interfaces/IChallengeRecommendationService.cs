using Common.Dto;
using Repositorys.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IChallengeRecommendationService : IService<ChallengeDto>
    {
        Task<string> GetRecommendedChallenges(string userPrompt);
    }
} 