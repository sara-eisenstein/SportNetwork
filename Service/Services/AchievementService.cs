
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
    public class AchievementService : IService<AchievementDto>
    {
        private readonly IRepository<Achievement> repository;
        private readonly IMapper mapper;

        public AchievementService(IRepository<Achievement> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<AchievementDto> GetAll()
        {
            return mapper.Map<List<AchievementDto>>(repository.GetAll());
        }

        public AchievementDto Get(int id)
        {
            return mapper.Map<AchievementDto>(repository.Get(id));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public AchievementDto Update(AchievementDto item,int id)
        {
            var entity = mapper.Map<Achievement>(item);
            return mapper.Map<AchievementDto>(repository.Update(entity,id));

        }

        public AchievementDto Add(AchievementDto item)
        {
            var entity = mapper.Map<Achievement>(item);
            return mapper.Map<AchievementDto>(repository.Add(entity));
        }
    }
}
