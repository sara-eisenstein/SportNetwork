using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public  class ExtenstionCommentRepository : ICommentRepository
    {
        private readonly IContext context;
        public ExtenstionCommentRepository(IContext context) { 
        this.context = context;
        }
        public List<Comment> GetCommentByPostId(int postId)
        {
          return  context.comments.Where(x=>x.PostId == postId).ToList(); 
        }
    }
}
