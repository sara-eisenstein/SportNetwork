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
    public class ExtensionUserService:IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public ExtensionUserService(IUserRepository userRepository, IMapper mapper)
        {

            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public UserDto GetUserByEmail(string email)
        {
            return mapper.Map<UserDto>(userRepository.GetUserByEmail(email));
        }
    }
}
