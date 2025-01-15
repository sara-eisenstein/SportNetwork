using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Entities
{
    public class Follower
    {
        public int FollowerId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; } // The user being followed
        public virtual User User { get; set; }

        [ForeignKey("FollowerUserId")]
        public int FollowerUserId { get; set; } // The user following
        public virtual User FollowerUser { get; set; }
    }
}
