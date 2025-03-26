using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IChatMessageService
    {
        List<ChatMessageDto> GetChatMessages(int userId, int otherUserId, int pageNumber);
        List<ChatUserDto> GetRecentChatUsersAsync(int userId);

    }
}
