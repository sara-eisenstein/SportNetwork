using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly IService<FollowerDto> _followerService;
        private readonly IFollowerService _extensionFollowerService;

        public FollowerController(IService<FollowerDto> followerService, IFollowerService extensionFollowerService)
        {
            _followerService = followerService;
            _extensionFollowerService = extensionFollowerService;
        }

        // GET api/<FollowerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid follower ID.");
                }

                var follower = _followerService.Get(id);
                if (follower == null)
                {
                    return NotFound($"Follower with ID {id} not found.");
                }

                return Ok(follower);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<FollowerController>
        [HttpPost]
        public IActionResult Post([FromForm] FollowerDto value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Invalid follower data.");
                }

                _followerService.Add(value);
                return Ok("Follower added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<FollowerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid follower ID.");
                }

                var existingFollower = _followerService.Get(id);
                if (existingFollower == null)
                {
                    return NotFound($"Follower with ID {id} not found.");
                }

                _followerService.Delete(id);
                return Ok("Follower deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // קבלת עוקבים לפי ID של משתמש
        [Authorize]
        [HttpGet("user/{userId}/followers")]
        public IActionResult GetFollowersByUserId(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { message = "Invalid user ID." });
                }

                var followers = _extensionFollowerService.GetFollowersByUserId(userId);
                if (followers == null || !followers.Any())
                {
                    return NotFound(new { message = "No followers found for this user." });
                }

                return Ok(followers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
