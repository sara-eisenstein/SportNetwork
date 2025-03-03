using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IService<CommentDto> _commentservice;
        private readonly IcommentService _commentservice2; 
        public CommentController(IService<CommentDto> commentservice, IcommentService commentservice2)
        {
            _commentservice = commentservice;
            _commentservice2 = commentservice2;
        }

        // GET: api/<CommentController>
        [HttpGet]
        public List<CommentDto> Get()
        {
            return _commentservice.GetAll();
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public CommentDto Get(int id)
        {
            return _commentservice.Get(id);
        }
        // GET api/<CommentController>/5
        [HttpGet("/getcommentByPostId/")]
        public List<CommentDto> GetCommentByPostId(int postId)
        {

           return _commentservice2.GetCommentByPostId(postId);
        }
        // POST api/<CommentController>
        [HttpPost]
        public void Post([FromForm] CommentDto value)
        {
            _commentservice.Add(value);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] CommentDto value)
        {
            _commentservice.Update(value, id);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _commentservice.Delete(id); 
        }
    }
}
