using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IService<PostDto> _postDervice;
        public static string _Directory = Environment.CurrentDirectory + "/media/";

        public PostController(IService<PostDto> postDervice)
        {
            _postDervice = postDervice;
        }

        // GET: api/<PostController>
        [HttpGet]
        public List<PostDto> Get()
        {
            return _postDervice.GetAll();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public PostDto Get(int id)
        {
            return _postDervice.Get(id);
        }
        [HttpGet("getimage/{id}")]
        public IActionResult GetImage(int id)
        {

            PostDto p = _postDervice.Get(id);
            return File(p.Media, "image/jpg");
        }

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromForm] PostDto value)
        {
            var filePath = Path.Combine
                (Environment.CurrentDirectory, "media/", value.File.FileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                value.File.CopyTo(fs);
            }
            _postDervice.Add(value);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] PostDto value)
        {
            _postDervice.Update(value, id);
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _postDervice.Delete(id);
        }
    }
}
