using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                if (value == null)
                {
                    return BadRequest("Invalid challenge participant data.");
                }

                _challengeParticipantService.Add(value);
                return Ok("Challenge participant added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<ChallengeParticipantController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] ChallengeParticipantDto value)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid challenge participant ID.");
                }

                var existingParticipant = _challengeParticipantService.Get(id);
                if (existingParticipant == null)
                {
                    return NotFound($"Challenge participant with ID {id} not found.");
                }

                _challengeParticipantService.Update(value, id);
                return Ok("Challenge participant updated successfully.");
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
                if (id <= 0)
                {
                    return BadRequest("Invalid challenge participant ID.");
                }

                var existingParticipant = _challengeParticipantService.Get(id);
                if (existingParticipant == null)
                {
                    return NotFound($"Challenge participant with ID {id} not found.");
                }

                _challengeParticipantService.Delete(id);
                return Ok("Challenge participant deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
