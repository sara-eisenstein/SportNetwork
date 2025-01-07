using Repositorys.Entities;
using Repositorys.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Rpository
{
    public class PostRepository: Irepository<Post>
    {
        private readonly IContext context;

        public PostRepository(IContext context)
        {
            this.context = context;
        }

        public Post Add(Post item)
        {
            context.posts.Add(item);
            context.Save();
            return item;
        }

        public void Delete(int id)
        {
            var post = Get(id);
            if (post != null)
            {
                context.posts.Remove(post);
                context.Save();
            }
        }

        public Post Get(int id)
        {
            return context.posts.FirstOrDefault(x => x.PostId == id);
        }

        public List<Post> GetAll()
        {
            return context.posts.ToList();
        }

        public Post Update(Post item)
        {
            var existingPost = Get(item.PostId);
            if (existingPost != null)
            {
                existingPost.Content = item.Content;
                existingPost.Media = item.Media;
                existingPost.Likes = item.Likes;
                existingPost.CreatedDate = item.CreatedDate;
                context.Save();
            }
            return existingPost;
        }
    }
}
