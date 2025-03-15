using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserDto> _userService;
        private readonly IUserService _extensionUserService;

        public static string _Directory = Environment.CurrentDirectory + "/media/";

        public UserController(IService<UserDto> userService, IUserService extensionUserService)
        {
            this._userService = userService;
            _extensionUserService = extensionUserService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _userService.GetAll();
                if (users == null || users.Count == 0)
                {
                    return NotFound("No users found.");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/<UserController>/5
        [Authorize]
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

                var user = _userService.Get(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                if (userId != id.ToString())
                {
                    return Forbid();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromForm] UserDto value)
        {
            try
            {
                if (value == null || value.File == null)
                {
                    return BadRequest("Invalid user data or file is missing.");
                }

                var filePath = Path.Combine(Environment.CurrentDirectory, "media/", value.File.FileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    value.File.CopyTo(fs);
                }

                _userService.Add(value);
                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] UserDto value)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated.");
                }


                if (userId != id.ToString())
                {
                    return Forbid();
                }

                var existingUser = _userService.Get(id);
                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                _userService.Update(value, id);
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        // GET User Image
        [HttpGet("/getUserImage/{id}")]
        public IActionResult GetImage(int id)
        {
            try
            {
                var user = _userService.Get(id);
                if (user == null )
                {
                    return NotFound("User not found.");
                }

                return File(user.ProfilePicture, "image/jpg");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{userId}/public")]
        public IActionResult GetPublicUserDetails(int userId)
        {
            try
            {
                var userPublicDetails = _extensionUserService.GetPublicUderDetails(userId);

                if (userPublicDetails == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }

                return Ok(userPublicDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
