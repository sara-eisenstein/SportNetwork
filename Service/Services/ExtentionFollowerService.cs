using AutoMapper;
using Common.Dto;
using Repositorys.Entities;
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
    public class ExtentionFollowerService : IFollowerService
    {
        private readonly IFollowerRepository followerRepository;
        private readonly IMapper mapper;

        public ExtentionFollowerService(IFollowerRepository followerRepository, IMapper mapper)
        {
            this.followerRepository = followerRepository;
            this.mapper = mapper;
        }

        public List<userPublicDto> GetFollowersByUserId(int userId)
        {
            return mapper.Map<List<userPublicDto>>(followerRepository.GetFollowersByUserId(userId));

        }

        public List<userPublicDto> GetFollowingByUserId(int userId)
        {
            return mapper.Map<List<userPublicDto>>(followerRepository.GetFollowingByUserId(userId));

        }

        public bool UnfollowUser(int userId, int unfollowUserId)
        {
            return followerRepository.UnfollowUser(userId, unfollowUserId);
        }

    }
}
