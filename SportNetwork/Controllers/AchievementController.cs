using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositorys.Entities;
using Service.Interfaces;
using System.Security.Claims;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class AchievementController : ControllerBase
    {
        private readonly IService<AchievementDto> _achievementService;
        private readonly IAchievementService _extentionAchievementService;


        public AchievementController(IService<AchievementDto> service, IAchievementService extentionAchievementService)
        {
            _achievementService = service;
            _extentionAchievementService = extentionAchievementService;
        }

        // POST api/<AchievementController>
        [HttpPost]
        public IActionResult Post([FromForm] AchievementDto value)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (value == null)
                {
                    return BadRequest("Invalid achievement data.");
                }


                if (userIdFromToken != value.UserId.ToString())
                {
                    return Forbid("You are not authorized.");
                }
                _achievementService.Add(value);
                return Ok("Achievement added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        [HttpGet("user/{userId}")]
        public IActionResult GetAchievementsByUserId(int userId)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID.");
                }
                if (userIdFromToken != userId.ToString())
                {
                    return Forbid("You are not authorized to access this achievements.");
                }
                var achievements = _extentionAchievementService.GetAchievementsByUserId(userId);
                if (achievements == null || !achievements.Any())
                {
                    return NotFound($"No achievements found for user ID {userId}.");
                }

                return Ok(achievements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
