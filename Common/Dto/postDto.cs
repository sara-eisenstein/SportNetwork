using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Media { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
