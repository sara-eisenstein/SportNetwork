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
    public class ChallengeParticipantService : IService<ChallengeParticipantDto>
    {
        private readonly IRepository<ChallengeParticipant> repository;
        private readonly IMapper mapper;

        public ChallengeParticipantService(IRepository<ChallengeParticipant> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<ChallengeParticipantDto> GetAll()
        {
            return mapper.Map<List<ChallengeParticipantDto>>(repository.GetAll());
        }

        public ChallengeParticipantDto Get(int id)
        {
            return mapper.Map<ChallengeParticipantDto>(repository.Get(id));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public ChallengeParticipantDto Update(ChallengeParticipantDto item, int id)
        {
            var entity = mapper.Map<ChallengeParticipant>(item);
            return mapper.Map<ChallengeParticipantDto>(repository.Update(entity, id));
        }

        public ChallengeParticipantDto Add(ChallengeParticipantDto item)
        {
            var entity = mapper.Map<ChallengeParticipant>(item);
            return mapper.Map<ChallengeParticipantDto>(repository.Add(entity));
        }
    }

}
