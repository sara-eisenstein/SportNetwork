using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using SportNetwork.Models.AI;
using System.ComponentModel.DataAnnotations;

namespace SportNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChallengeRecommendationController : ControllerBase
    {
        private readonly IChallengeRecommendationService _recommendationService;

        public ChallengeRecommendationController(IChallengeRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        /// <summary>
        /// מקבל פרומפט מהמשתמש ומחזיר רשימת ID של אתגרים מתאימים
        /// </summary>
        /// <param name="request">הבקשה המכילה את הפרומפט ורשימת האתגרים הזמינים</param>
        /// <returns>רשימת ID של אתגרים מתאימים</returns>
        /// 
        [Authorize]
        [HttpPost("recommend")]
        public async Task<ActionResult<List<int>>> GetRecommendations([FromBody, Required] ChallengeRecommendationRequest request)
        {
            try
            {
                var recommendedIds = await _recommendationService.GetRecommendedChallenges(
                    request.UserPrompt
                );

                return Ok(recommendedIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting recommendations: {ex.Message}");
            }
        }
    }
} 