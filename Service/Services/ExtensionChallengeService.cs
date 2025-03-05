using AutoMapper;
using Common.Dto;
using Repositorys.Interface;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ExtensionChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeService;   
        private readonly IMapper _mapper;   
        public ExtensionChallengeService(IChallengeRepository challengeService, IMapper mapper)
        {
            _challengeService = challengeService;
            _mapper = mapper;
        }
        public List<ChallengeDto> GetChallengesByUserId(int userId)
        {
            return _mapper.Map<List<ChallengeDto>>(_challengeService.GetChallengesByUserId(userId));
        }
    }
}
