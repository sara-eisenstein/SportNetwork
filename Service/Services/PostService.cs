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
    public class PostService : IService<PostDto>
    {
        private readonly IRepository<Post> repository;
        private readonly IMapper mapper;

        public PostService(IRepository<Post> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public PostDto Add(PostDto item)
        {
            var entity = mapper.Map<Post>(item);
            return mapper.Map<PostDto>(repository.Add(entity));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public PostDto Get(int id)
        {
            return mapper.Map<PostDto>(repository.Get(id));
        }

       

        public List<PostDto> GetAll()
        {
            return mapper.Map<List<PostDto>>(repository.GetAll());
        }

        public PostDto Update(PostDto item)
        {
            var entity = mapper.Map<Post>(item);
            return mapper.Map<PostDto>(repository.Update(entity));
        }

       
    }
}
