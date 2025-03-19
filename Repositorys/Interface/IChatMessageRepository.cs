using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Interface
{
    public interface IChatMessageRepository
    {
        List<ChatMessage> GetChatMessages(int userId, int otherUserId, int pageNumber);
    }
}
