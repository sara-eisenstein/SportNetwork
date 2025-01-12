using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class Comment_Dto
    {
        public int CommentId { get; set; }
        public int PostId { get; set; } // Foreign key to Post
        public Post Post { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
