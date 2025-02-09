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
        private readonly IConfiguration _configuration;
        public LoginController(IService<UserDto> userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        private string Generate(UserDto user)
        {
            var secretKay = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Kay"]));
            var corditionl=new SigningCredentials(secretKay,SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Name,user.LastName),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.FirstName)
                
            };
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims,expires:DateTime.Now.AddDays(1),signingCredentials: corditionl);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserDto Authenticate(string firstName,string lastName, string password)
        {
            return _userService.GetAll().FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName && x.PasswordHash == password);
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

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromBody] string firstName, [FromBody]string lastName, [FromBody]string password)
        {
            var user=Authenticate(firstName,lastName,password);
            if (user != null) {
                var token = Generate(user);
                return Ok(token);   
            }
            return BadRequest("user do not exist");
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
