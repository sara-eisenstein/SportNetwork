using AutoMapper;
using Common.Dto;
using Repositorys.Interface;
using Repositorys.Rpository;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ExtensionAchievementService:IAchievementService
    {
        private readonly IAchievementRepository achievementRepository;
        private readonly IMapper mapper;

        public ExtensionAchievementService(IAchievementRepository achievementRepository, IMapper mapper)
        {

            this.achievementRepository = achievementRepository;
            this.mapper = mapper;
        }

        public List<AchievementDto> GetAchievementsByUserId(int userId)
        {
            return mapper.Map<List<AchievementDto>>(achievementRepository.GetAchievementsByUserId(userId));

        }
    }
}
