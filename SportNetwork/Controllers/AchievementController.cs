using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IService<AchievementDto> _achievementService;

        public AchievementController(IService<AchievementDto> service)
        {
            _achievementService = service;
        }

        // POST api/<AchievementController>
        [HttpPost]
        public IActionResult Post([FromForm] AchievementDto value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Invalid achievement data.");
                }

                _achievementService.Add(value);
                return Ok("Achievement added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<AchievementController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] AchievementDto value)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid achievement ID.");
                }

                var existingAchievement = _achievementService.Get(id);
                if (existingAchievement == null)
                {
                    return NotFound($"Achievement with ID {id} not found.");
                }

                _achievementService.Update(value, id);
                return Ok("Achievement updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<AchievementController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid achievement ID.");
                }

                var existingAchievement = _achievementService.Get(id);
                if (existingAchievement == null)
                {
                    return NotFound($"Achievement with ID {id} not found.");
                }

                _achievementService.Delete(id);
                return Ok("Achievement deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
