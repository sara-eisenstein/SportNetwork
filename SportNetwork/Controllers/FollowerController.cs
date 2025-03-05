using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    
        // GET: api/<FollowerController>
        [HttpGet]
        public List<FollowerDto> Get()
        {
            return _followerService.GetAll();
        }

        // GET api/<FollowerController>/5
        [HttpGet("{id}")]
        public FollowerDto Get(int id)
        {
            return _followerService.Get(id);
        }

        // POST api/<FollowerController>
        [HttpPost]
        public void Post([FromForm] FollowerDto value)
        {
            _followerService.Add(value);
        }

        // PUT api/<FollowerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] FollowerDto value)
        {
            _followerService.Update(value, id);

        }

        // DELETE api/<FollowerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _followerService.Delete(id);    
        }

        [HttpGet("user/{userId}/followers")]
        public IActionResult GetFollowersByUserId(int userId)
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


    }
}
