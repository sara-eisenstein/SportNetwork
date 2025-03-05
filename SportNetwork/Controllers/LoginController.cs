using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IService<UserDto> _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IService<UserDto> userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        private string Generate(UserDto user)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Kay"]));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                };
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating JWT token: {ex.Message}");
            }
        }

        private UserDto Authenticate(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Email and password must be provided.");
                }

                var user = _userService.GetAll().FirstOrDefault(x => x.Email == email && x.PasswordHash == password);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during authentication: {ex.Message}");
            }
        }

        // GET: api/<LoginController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(new string[] { "value1", "value2" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok("value");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return BadRequest("Email and password are required.");
                }

                var user = Authenticate(email, password);
                if (user != null)
                {
                    var token = Generate(user);
                    return Ok(token);
                }

                return Unauthorized("User does not exist or incorrect credentials.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            try
            {
                return Ok($"Updated value: {value}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok($"Deleted item with ID {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
