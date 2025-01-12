using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class FollowerDto
    {
        public int FollowerId { get; set; }
        public int UserId { get; set; } // The user being followed
        public User User { get; set; }
        public int FollowerUserId { get; set; } // The user following
        public User FollowerUser { get; set; }
    }
}
