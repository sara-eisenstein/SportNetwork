using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Entities
{
    public class Achievement
    {
        public int AchievementId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User
        public virtual User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateEarned { get; set; }
    }
}
