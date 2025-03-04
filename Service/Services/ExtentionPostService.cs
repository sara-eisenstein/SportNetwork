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
    public class ExtentionPostService:IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public ExtentionPostService( IPostRepository postRepository, IMapper mapper)
        {
            
            this.postRepository = postRepository;
            this.mapper = mapper;
        }


        public PostDto AddLike(int userId, int postId)
        {
            return mapper.Map<PostDto>(postRepository.AddLike(userId,postId));
        }

   

        public List<PostDto> GetByUserId(int id)
        {
            return mapper.Map<List<PostDto>>(postRepository.GetByUserId(id));
        }

        public int GetLikeCount(int postId)
        {
            return postRepository.GetLikeCount(postId);
        }

        public PostDto RemoveLike(int userId, int postId)
        {
            return mapper.Map<PostDto>(postRepository.RemoveLike(userId, postId));
        }
    }
}
