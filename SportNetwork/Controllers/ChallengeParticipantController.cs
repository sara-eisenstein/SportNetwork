using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;
using System.Linq;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChallengeParticipantController : ControllerBase
    {
        private readonly IService<ChallengeParticipantDto> _challengeParticipantService;

        public ChallengeParticipantController(IService<ChallengeParticipantDto> service)
        {
            _challengeParticipantService = service;
        }

        // ✅ פעולה שמחזירה את כל ההשתתפויות של משתמש לפי ID
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            try
            {
                if (userId <= 0)
                    return BadRequest("Invalid user ID.");

                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdFromToken != userId.ToString())
                    return Forbid("You are not authorized to access these participations.");

                var userParticipations = _challengeParticipantService
                    .GetAll()
                    .Where(p => p.UserId == userId)
                    .ToList();

                if (!userParticipations.Any())
                    return NotFound($"No challenge participations found for user ID {userId}.");

                return Ok(userParticipations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ✅ פעולה שמחזירה משתתף לפי ID
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid challenge participant ID.");

                var participant = _challengeParticipantService.Get(id);
                if (participant == null)
                    return NotFound($"Challenge participant with ID {id} not found.");

                return Ok(participant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ✅ פעולה להוספת משתתף
        [HttpPost]
        public IActionResult Post([ FromForm] ChallengeParticipantDto value)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (value == null)
                    return BadRequest("Invalid challenge participant data.");

                if (userIdFromToken != value.UserId.ToString())
                    return Forbid("You are not authorized to perform this action.");

                _challengeParticipantService.Add(value);
                return StatusCode(201, "Challenge participant added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ✅ מחיקת משתתף לפי ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (id <= 0)
                    return BadRequest("Invalid challenge participant ID.");

                var existingParticipant = _challengeParticipantService.Get(id);
                if (existingParticipant == null)
                    return NotFound($"Challenge participant with ID {id} not found.");

                if (userIdFromToken != existingParticipant.UserId.ToString())
                    return Forbid("You are not authorized to delete this challenge participation.");

                _challengeParticipantService.Delete(id);
                return Ok("Challenge participant deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ✅ עדכון התקדמות של אתגר
        [HttpPut("{id}/progress")]
        public IActionResult UpdateProgress(int id, [FromForm] ChallengeParticipantDto value)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (id <= 0)
                    return BadRequest("Invalid challenge participant ID.");

                if (value == null)
                    return BadRequest("Invalid challenge participant data.");

                var existingParticipant = _challengeParticipantService.Get(id);
                if (existingParticipant == null)
                    return NotFound($"Challenge participant with ID {id} not found.");

                if (userIdFromToken != existingParticipant.UserId.ToString())
                    return Forbid("You are not authorized to update this challenge participation.");

                existingParticipant.Progress = value.Progress;
                _challengeParticipantService.Update(existingParticipant, id);

                return Ok("Challenge progress updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
