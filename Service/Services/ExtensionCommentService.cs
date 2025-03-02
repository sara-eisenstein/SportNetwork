using AutoMapper;
using Common.Dto;
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
    public class ExtensionCommentService:IcommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper mapper;
        public ExtensionCommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            this.mapper = mapper;
        }

        public List<CommentDto> GetCommentByPostId(int postId)
        {
            return mapper.Map<List<CommentDto>>(_commentRepository.GetCommentByPostId(postId));
        }
    }
}
