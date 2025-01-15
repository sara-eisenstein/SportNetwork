using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; } // Foreign key to Post
        public virtual Post Post { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User
        public virtual User User { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
