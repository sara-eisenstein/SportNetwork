using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Interface
{
    public interface IPostRepository
    {
        Post AddLike(int userId, int postId);
        Post RemoveLike(int userId, int postId);

        int GetLikeCount(int postId);
        List<Post> GetByUserId(int id);
    }
}
