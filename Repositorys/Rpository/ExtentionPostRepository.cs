using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class ExtentionPostRepository : IPostRepository
    {
        private readonly IContext context;

        public ExtentionPostRepository(IContext context)
        {
            this.context = context;
        }
        public Post AddLike(int userId, int postId)
        {
            var post = context.posts.FirstOrDefault(p => p.PostId == postId);

            if (post == null)
                throw new Exception("Post not found.");

            if (post.Likes.Contains(userId)) //מונע אפשרות שמשתמש יתן 2 לייקים לאותו פוסט
                throw new Exception("User has already liked this post.");

            post.Likes.Add(userId);
            context.Save();

            return post;
        }

        public Post RemoveLike(int userId, int postId)
        {
            var post = context.posts.FirstOrDefault(p => p.PostId == postId);

            if (post == null)
                throw new Exception("Post not found.");

            if (!post.Likes.Contains(userId))
                throw new Exception("User has not liked this post.");

            post.Likes.Remove(userId);
            context.Save();

            return post;    
        }

        public int GetLikeCount(int postId)
        {
            var post = context.posts.FirstOrDefault(p => p.PostId == postId);

            if (post == null)
                throw new Exception("Post not found.");

            return post.Likes.Count;
        }





        public List<Post> GetByUserId(int userId)
        {
            return context.posts.Where(x => x.UserId == userId).ToList();
        }
    }
}
