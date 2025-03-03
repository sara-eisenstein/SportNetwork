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
        public static string _Directory = Environment.CurrentDirectory + "/media/";
        private readonly IPostService _ExtentionPostService;

        
        
        public PostController(IService<PostDto> postDervice, IPostService ExtentionPostService)
        {
            _postDervice = postDervice;
            _ExtentionPostService = ExtentionPostService;

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
            _postDervice.Update(value);
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _postDervice.Delete(id);
        }

        [HttpGet("/getPostImage/{id}")]
        public IActionResult GetImage(int id)
        {
            PostDto p = _postDervice.Get(id);
            return File(p.Media, "image/jpg");
        }


        // הוספת לייק
        [HttpPost("{postId}/like/{userId}")]
        public IActionResult LikePost(int postId, int userId)
        {
            try
            {
                _ExtentionPostService.AddLike(userId, postId);
                return Ok("Like added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // הסרת לייק
        [HttpDelete("{postId}/like/{userId}")]
        public IActionResult UnlikePost(int postId, int userId)
        {
            try
            {
                _ExtentionPostService.RemoveLike(userId, postId);
                return Ok("Like removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // קבלת כמות לייקים
        [HttpGet("{postId}/likes")]
        public IActionResult GetLikeCount(int postId)
        {
            try
            {
                int count = _ExtentionPostService.GetLikeCount(postId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
