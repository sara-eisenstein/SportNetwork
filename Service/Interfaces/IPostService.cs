using Common.Dto;
using Repositorys.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPostService
    {
        PostDto AddLike(int userId, int postId);
        PostDto RemoveLike(int userId, int postId);

        int GetLikeCount(int postId);
        List<PostDto> GetByUserId(int id);
    }
}
