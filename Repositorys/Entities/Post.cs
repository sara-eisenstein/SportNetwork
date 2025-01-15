using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Entities
{
    public class Post
    {
        public int PostId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User
        public virtual User User { get; set; }

        public string Content { get; set; }
        public string Media { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}
