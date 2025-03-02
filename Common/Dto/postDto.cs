using Microsoft.AspNetCore.Http;
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

        public List<int> Likes { get; set; } = new List<int>();
        public DateTime CreatedDate { get; set; }

        public Byte[]? Media { get; set; }
        public IFormFile File { get; set; }
    }
}
