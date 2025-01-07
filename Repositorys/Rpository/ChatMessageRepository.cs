using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ChatMessageRepository:Irepository<ChatMessage>
    {
        private readonly IContext context;

        public ChatMessageRepository(IContext context)
        {
            this.context = context;
        }

        public ChatMessage Add(ChatMessage item)
        {
            context.chatMessages.Add(item);
            context.Save();
            return item;
        }

        public void Delete(int id)
        {
            var message = Get(id);
            if (message != null)
            {
                context.chatMessages.Remove(message);
                context.Save();
            }
        }

        public ChatMessage Get(int id)
        {
            return context.chatMessages.FirstOrDefault(x => x.MessageId == id);
        }

        public List<ChatMessage> GetAll()
        {
            return context.chatMessages.ToList();
        }

        public ChatMessage Update(ChatMessage item)
        {
            var existingMessage = Get(item.MessageId);
            if (existingMessage != null)
            {
                existingMessage.SenderId = item.SenderId;
                existingMessage.RecipientId = item.RecipientId;
                existingMessage.MessageContent = item.MessageContent;
                existingMessage.SentDate = item.SentDate;
                context.Save();
            }
            return existingMessage;
        }
    }
}
