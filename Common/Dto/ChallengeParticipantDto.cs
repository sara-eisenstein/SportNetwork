using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ChallengeParticipantDto
    {
        public int ChallengeParticipantId { get; set; }
        public int ChallengeId { get; set; }
        public int UserId { get; set; }
        public string Progress { get; set; }
    }
}
