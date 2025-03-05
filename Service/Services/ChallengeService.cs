using AutoMapper;
using Repositorys.Entities;
using Repositorys.Interface;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;


namespace Service.Services
{
    public class ChallengeService : IService<ChallengeDto>
    {
        private readonly IRepository<Challenge> repository;
        private readonly IMapper mapper;

        public ChallengeService(IRepository<Challenge> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<ChallengeDto> GetAll()
        {
            return mapper.Map<List<ChallengeDto>>(repository.GetAll());
        }

        public ChallengeDto Get(int id)
        {
            return mapper.Map<ChallengeDto>(repository.Get(id));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public ChallengeDto Update(ChallengeDto item, int id)
        {
            var entity = mapper.Map<Challenge>(item);
            return mapper.Map<ChallengeDto>(repository.Update(entity, id));
        }

        public ChallengeDto Add(ChallengeDto item)
        {
            var entity = mapper.Map<Challenge>(item);
            return mapper.Map<ChallengeDto>(repository.Add(entity));
        }
    }
}
