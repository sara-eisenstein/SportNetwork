using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        //TODO לבדוק שהצורה הזאת תקינה ולא שצריך לעשות את הבדיקה של ההרשאה בפונקציה חיצונית או משהו


        // PUT api/<UserController>/5
        [HttpPut("{id}")]

        [Authorize]
        public void Put(int id, [FromForm] UserDto value)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != id.ToString())

                throw new Exception("oops!"); // לא אתה? נחסום את הגישה

            else
            {
                _userService.Update(value);

            }


        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.Delete(id);    
        }
    }
}
