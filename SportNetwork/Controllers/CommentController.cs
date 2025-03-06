using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;

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
        //[HttpGet]
        //public List<CommentDto> Get()
        //{
        //    return _commentservice.GetAll();
        //}

        // GET api/<CommentController>/5
        //[HttpGet("{id}")]
        //public CommentDto Get(int id)
        //{
        //    return _commentservice.Get(id);
        //}
        // GET api/<CommentController>/5
        [HttpGet("/getcommentByPostId/")]
        public List<CommentDto> GetCommentByPostId(int postId)
        {

           return _commentservice2.GetCommentByPostId(postId);
        }
        // POST api/<CommentController>
        [HttpPost]
        [Authorize]
        public void Post([FromForm] CommentDto value)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != value.UserId.ToString())
                _commentservice.Add(value);
            else
                throw new Exception("not connect");
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromForm] CommentDto value)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != value.UserId.ToString())
                _commentservice.Update(value, id);
            else throw new Exception("user not conncet");
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
            var value=_commentservice.Get(id);
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != value.UserId.ToString())
                _commentservice.Delete(id);
            else throw new Exception("user not connect");
        }
    }
}
