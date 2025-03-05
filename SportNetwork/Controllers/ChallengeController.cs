using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IService<ChallengeDto> _challengeService;
        private readonly IChallengeService _challengeService2;

        public ChallengeController(IService<ChallengeDto> service, IChallengeService challengeService2)
        {
            _challengeService = service;
            _challengeService2 = challengeService2;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
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
                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID.");
                }

                var challenges = _challengeService2.GetChallengesByUserId(userId);
                if (challenges == null || !challenges.Any())
                {
                    return NotFound($"No challenges found for user ID {userId}.");
                }

                return Ok(challenges);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
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
