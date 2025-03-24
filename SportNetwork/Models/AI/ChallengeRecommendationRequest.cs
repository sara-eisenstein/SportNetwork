using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repositorys.Entities;

namespace SportNetwork.Models.AI
{
    public class ChallengeRecommendationRequest
    {
      
        [Required]
        public string UserPrompt { get; set; }
    }

    public class Challenge
    {
        public int ChallengeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
} 