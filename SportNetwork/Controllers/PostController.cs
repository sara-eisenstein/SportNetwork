using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IService<PostDto> _postDervice;
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

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromBody] PostDto value)
        {
            _postDervice.Add(value);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PostDto value)
        {
            _postDervice.Update(value);
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _postDervice.Delete(id);
        }
    }
}
