using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.Entities
{
    public class ChallengeParticipant
    {
        public int ChallengeParticipantId { get; set; }

        [ForeignKey("Challenge")]
        public int ChallengeId { get; set; } // Foreign key to Challenge
        public virtual Challenge Challenge { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key to User
        public virtual User User { get; set; }

        public string Progress { get; set; }
    }
}
