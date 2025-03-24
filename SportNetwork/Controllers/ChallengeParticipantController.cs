using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositorys.Entities;
using Service.Interfaces;
using Service.Services;
using System.Security.Claims;

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

        // GET api/<ChallengeParticipantController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid challenge participant ID.");
                }

                var participant = _challengeParticipantService.Get(id);
                if (participant == null)
                {
                    return NotFound($"Challenge participant with ID {id} not found.");
                }

                return Ok(participant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<ChallengeParticipantController>
        [HttpPost]
        public IActionResult Post([FromForm] ChallengeParticipantDto value)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                

                if (value == null)
                {
                    return BadRequest("Invalid challenge participant data.");
                }
                if (userIdFromToken != value.ChallengeParticipantId.ToString())
                {
                    return Forbid("You are not authorized to do it.");
                }

                _challengeParticipantService.Add(value);
                return Ok("Challenge participant added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        
        // DELETE api/<ChallengeParticipantController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (id <= 0)
                {
                    return BadRequest("Invalid challenge participant ID.");
                }

                var existingParticipant = _challengeParticipantService.Get(id);
                if (existingParticipant == null)
                {
                    return NotFound($"Challenge participant with ID {id} not found.");
                }


                if (userIdFromToken != existingParticipant.ChallengeParticipantId.ToString())
                {
                    return Forbid("You are not authorized to access this challenges.");
                }

                _challengeParticipantService.Delete(id);
                return Ok("Challenge participant deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}/progress")]
        public IActionResult UpdateProgress(int id, [FromForm] ChallengeParticipantDto value)
        {
            try
            {
                var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (id <= 0)
                {
                    return BadRequest("Invalid challenge participant ID.");
                }

                if (value == null)
                {
                    return BadRequest("Invalid challenge participant data.");
                }

                var existingParticipant = _challengeParticipantService.Get(id);
                if (existingParticipant == null)
                {
                    return NotFound($"Challenge participant with ID {id} not found.");
                }

                // השוואה נכונה של משתמש מה-token למשתמש המשויך לרשומה
                if (userIdFromToken != existingParticipant.UserId.ToString())
                {
                    return Forbid();
                }

                // עדכון רק של שדה ההתקדמות
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
