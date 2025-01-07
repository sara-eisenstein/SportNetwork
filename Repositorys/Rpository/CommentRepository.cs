using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class CommentRepository:Irepository<Comment>
    {
        private readonly IContext context;

        public CommentRepository(IContext context)
        {
            this.context = context;
        }

        public Comment Add(Comment item)
        {
            context.comments.Add(item);
            context.Save();
            return item;
        }

        public void Delete(int id)
        {
            var comment = Get(id);
            if (comment != null)
            {
                context.comments.Remove(comment);
                context.Save();
            }
        }

        public Comment Get(int id)
        {
            return context.comments.FirstOrDefault(x => x.CommentId == id);
        }

        public List<Comment> GetAll()
        {
            return context.comments.ToList();
        }

        public Comment Update(Comment item)
        {
            var existingComment = Get(item.CommentId);
            if (existingComment != null)
            {
                existingComment.Content = item.Content;
                existingComment.CreatedDate = item.CreatedDate;
                context.Save();
            }
            return existingComment;
        }
    }
}
