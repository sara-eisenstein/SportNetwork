using AutoMapper;
using Repositorys.Entities;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;
using Repositorys.Interface;



namespace Service.Services
{
    public class CommentService : IService<CommentDto>
    {
        private readonly IRepository<Comment> repository;
        private readonly IMapper mapper;

        public CommentService(IRepository<Comment> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public CommentDto Add(CommentDto item)
        {
            var entity = mapper.Map<Comment>(item);
            return mapper.Map<CommentDto>(repository.Add(entity));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public CommentDto Get(int id)
        {
            return mapper.Map<CommentDto>(repository.Get(id));
        }

        public List<CommentDto> GetAll()
        {
            return mapper.Map<List<CommentDto>>(repository.GetAll());
        }

        public CommentDto Update(CommentDto item)
        {
            var entity = mapper.Map<Comment>(item);
            return mapper.Map<CommentDto>(repository.Update(entity));
        }
    }
}

