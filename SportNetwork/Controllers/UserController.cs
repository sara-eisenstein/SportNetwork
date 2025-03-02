using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IService<UserDto> _userService;
        public static string _Directory=Environment.CurrentDirectory+"/media/";
        public UserController(IService<UserDto> userService)
        {
            this._userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public List<UserDto> Get()
        {
            return _userService.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserDto Get(int id)
        {
            return _userService.Get(id);
        }
   

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromForm] UserDto value)
        {
            var filePath = Path.Combine
                (Environment.CurrentDirectory, "media/", value.File.FileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create)) {
                value.File.CopyTo(fs);
            }
                _userService.Add(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] UserDto value)
        {
            _userService.Update(value, id);

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.Delete(id);    
        }

        [HttpGet("/getUserImage/{id}")]
        public IActionResult GetImage(int id)
        {
            UserDto u = _userService.Get(id);
            return File(u.ProfilePicture,"image/jpg");
        }
    }
}
