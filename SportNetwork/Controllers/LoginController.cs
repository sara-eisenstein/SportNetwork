using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IService<UserDto> _userService;
        private readonly ILoginService _tokenService;

        public LoginController(IService<UserDto> userService, ILoginService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        private UserDto Authenticate(string firstName, string lastName, string password)
        {
            return _userService.GetAll().FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName && x.PasswordHash == password);
        }

        [HttpPost]
        public IActionResult Post([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string password)
        {
            var user = Authenticate(firstName, lastName, password);
            if (user != null)
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(token);
            }
            return BadRequest("User does not exist");
        }
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
