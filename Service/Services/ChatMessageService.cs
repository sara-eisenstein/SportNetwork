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
    public class ChatMessageService : IService<ChatMessageDto>
    {
        private readonly IRepository<ChatMessage> repository;
        private readonly IMapper mapper;

        public ChatMessageService(IRepository<ChatMessage> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<ChatMessageDto> GetAll()
        {
            return mapper.Map<List<ChatMessageDto>>(repository.GetAll());
        }

        public ChatMessageDto Get(int id)
        {
            return mapper.Map<ChatMessageDto>(repository.Get(id));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public ChatMessageDto Update(ChatMessageDto item)
        {
            var entity = mapper.Map<ChatMessage>(item);
            return mapper.Map<ChatMessageDto>(repository.Update(entity));
        }

        public ChatMessageDto Add(ChatMessageDto item)
        {
            var entity = mapper.Map<ChatMessage>(item);
            return mapper.Map<ChatMessageDto>(repository.Add(entity));
        }
    }
}
