using AutoMapper;
using Repositorys.Entities;
using Repositorys.Interface;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FollowerService : IService<FollowerDto>
    {
        private readonly IRepository<Follower> repository;
        private readonly IMapper mapper;
        public FollowerService(IRepository<Follower> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
    
        public FollowerDto Add(FollowerDto item)
        {
            var entity = mapper.Map<Follower>(item);
            return mapper.Map<FollowerDto>(repository.Add(entity));
        }

        public void Delete(int id)
        {
           repository.Delete(id);
        }

        public FollowerDto Get(int id)
        {
            return mapper.Map<FollowerDto>(Get(id));
        }

        public List<FollowerDto> GetAll()
        {
            return mapper.Map<List<FollowerDto>>(GetAll());
        }

        public FollowerDto Update(FollowerDto item)
        {
            var entity = mapper.Map<Follower>(item);
            return mapper.Map<FollowerDto>(repository.Update(entity));
        }
    }
}
