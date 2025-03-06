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
        private readonly IAchievementService _extentionAchievementService;


        public AchievementController(IService<AchievementDto> service, IAchievementService extentionAchievementService)
        {
<<<<<<< HEAD
            this._achievementService = service;

        }


        // GET: api/<AchievementController>
        //[HttpGet]
        //public List<AchievementDto> Get()
        //{
        //    return _achievementService.GetAll();
        //}

        // GET api/<AchievementController>/5
        //[HttpGet("{id}")]
        //public AchievementDto Get(int id)
        //{
        //    return _achievementService.Get(id);
        //}
=======
            _achievementService = service;
            _extentionAchievementService = extentionAchievementService;
        }
>>>>>>> 373b4ec11967f64dd9046533683847848a5db4fe

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

        [HttpGet("user/{userId}")]
        public IActionResult GetAchievementsByUserId(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID.");
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
