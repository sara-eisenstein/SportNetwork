using Common.Dto;
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

        public List<ChatUserDto> GetRecentChatUsersAsync(int userId)
        {
            var recentUsers = context.users
                .Select(user => new
                {
                    User = user,
                    LastMessageDate = context.chatMessages
                        .Where(m => (m.SenderId == user.UserId && m.RecipientId == userId) ||
                                    (m.RecipientId == user.UserId && m.SenderId == userId))
                        .OrderByDescending(m => m.SentDate)
                        .Select(m => m.SentDate)
                        .FirstOrDefault()
                })
                .OrderByDescending(u => u.LastMessageDate)  // ממיינים לפי התאריך האחרון
                .Select(u => new ChatUserDto
                {
                    UserId = u.User.UserId,
                    UserName = u.User.FirstName + " " + u.User.LastName,
                    ProfilePictureUrl = u.User.ProfilePicture,
                    // משנים את הערך של LastMessageDate ל-nullable
                    LastMessageDate = u.LastMessageDate  // אם אין הודעה, LastMessageDate יהיה null
                })
                .ToList();

            return recentUsers;
        }

    }
}
