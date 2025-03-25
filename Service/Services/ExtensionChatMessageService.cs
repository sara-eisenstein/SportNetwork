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
    public class ExtensionChatMessageService:IChatMessageService
    {
        private readonly IChatMessageRepository repository;
        private readonly IMapper mapper;
        public ExtensionChatMessageService(IChatMessageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }



        public List<ChatMessageDto> GetChatMessages(int userId, int otherUserId, int pageNumber)
        {
            return mapper.Map<List<ChatMessageDto>>(repository.GetChatMessages(userId, otherUserId, pageNumber));
        }

        public  List<ChatUserDto> GetRecentChatUsersAsync(int userId)
        {
            return repository.GetRecentChatUsersAsync(userId);
        }
    }

        
   
}
