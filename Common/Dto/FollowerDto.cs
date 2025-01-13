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
        public int UserId { get; set; }
        public int FollowerUserId { get; set; }
    }
}
