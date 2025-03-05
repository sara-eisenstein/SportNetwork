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
        public List<ChatMessage> GetChatMassages(int id)
        {
            return context.chatMessages.Where(x=>x.RecipientId == id||x.SenderId==id).OrderBy(x=>x.SentDate).ToList();
        }
    }
}
