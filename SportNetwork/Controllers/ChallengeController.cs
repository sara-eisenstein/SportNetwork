using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ChallengeController : ControllerBase
    {
        private readonly IService<ChallengeDto> _challengeService;
        private readonly IChallengeService _extensionChallengeService;

        public ChallengeController(IService<ChallengeDto> service, IChallengeService extensionChallengeService)
        {
            _challengeService = service;
            _extensionChallengeService = extensionChallengeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated.");
                }
                var challenges = _challengeService.GetAll();
                if (challenges == null || !challenges.Any())
                {
                    return NotFound("No challenges found.");
                }

                return Ok(challenges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated.");
                }

                if (id <= 0)
                {
                    return BadRequest("Invalid challenge ID.");
                }

                var challenge = _challengeService.Get(id);
                if (challenge == null)
                {
                    return NotFound($"Challenge with ID {id} not found.");
                }

                return Ok(challenge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/challengeToUser/")]
        public IActionResult GetChallengeToUser(int userId)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated.");
                }
                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID.");
                }

                var challenges = _extensionChallengeService.GetChallengesByUserId(userId);
                if (challenges == null || !challenges.Any())
                {
                    return NotFound($"No challenges found for user ID {userId}.");
                }
                if (userIdFromToken != userId.ToString())
                {
                    return Forbid();
                }
                return Ok(challenges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm] ChallengeDto value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Invalid challenge data.");
                }

                _challengeService.Add(value);
                return Ok("Challenge added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
