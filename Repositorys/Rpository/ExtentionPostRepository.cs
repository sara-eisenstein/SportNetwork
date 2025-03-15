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

            var likes = post.Likes.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            if (likes.Contains(userId))
                throw new Exception("User has already liked this post.");

            likes.Add(userId);
            post.Likes = string.Join(",", likes);

            context.Save();

            return post;
        }


        public Post RemoveLike(int userId, int postId)
        {
            var post = context.posts.FirstOrDefault(p => p.PostId == postId);

            if (post == null)
                throw new Exception("Post not found.");

            if (string.IsNullOrEmpty(post.Likes))
                return post;

            var likes = post.Likes.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            if (!likes.Contains(userId))
                return post;
            likes.Remove(userId);
            post.Likes = string.Join(",", likes);

            context.Save();

            return post;
        }


        public int GetLikeCount(int postId)
        {
            var post = context.posts.FirstOrDefault(p => p.PostId == postId);

            if (post == null)
                throw new Exception("Post not found.");

            return post.Likes == null ? 0 : post.Likes.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
        }




        public List<Post> GetByUserId(int userId)
        {
            return context.posts.Where(x => x.UserId == userId).ToList();
        }
    }
}
