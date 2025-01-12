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
    public class UserService : IService<UserDto>
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;
        public UserService(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
    
        public UserDto Add(UserDto item)
        {
            var entity = mapper.Map<User>(item);
            return mapper.Map<User>(repository.Add(entity));
        }

        public void Delete(int id)
        {
           repository.Delete(id);
        }

        public UserDto Get(int id)
        {
            return mapper.Map<UserDto>(repository.Get(id));
         
        }

        public List<UserDto> GetAll()
        {
            return mapper.Map<List<UserDto>>(repository.GetAll());
        }

        public UserDto Update(UserDto item)
        {
            var entity = mapper.Map<User>(item);
            return mapper.Map<User>(repository.Update(entity));
        }
    }
}
