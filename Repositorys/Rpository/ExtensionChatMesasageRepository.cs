using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ExtensionChatMesasageRepository:IChatMessageRepository
    {
        private readonly IContext context;
        public ExtensionChatMesasageRepository(IContext context)
        {
            this.context = context;
        }
        public List<ChatMessage> GetChatMessages(int userId, int otherUserId, int pageNumber)
        {
            int pageSize = 5;
            return context.chatMessages
                .Where(x =>
                    (x.SenderId == userId && x.RecipientId == otherUserId) ||
                    (x.SenderId == otherUserId && x.RecipientId == userId))
                .OrderByDescending(x => x.SentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
