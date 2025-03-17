using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositorys.Rpository;
using Service.Interfaces;
using Service.Services;
using System.IO;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IService<PostDto> _postService;
        private readonly IPostService _ExtentionPostService;

        public PostController(IService<PostDto> postService, IPostService ExtentionPostService)
        {
            _postService = postService;
            _ExtentionPostService = ExtentionPostService;
        }

        // GET: api/Post
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var posts = _postService.GetAll();
                if (posts == null || !posts.Any())
                {
                    return NotFound("No posts found.");
                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET api/Post/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var post = _postService.Get(id);
                if (post == null)
                {
                    return NotFound($"Post with ID {id} not found.");
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/Post
        [HttpPost]
        public IActionResult Post([FromForm] PostDto value)
        {
            try
            {
                if (value == null || value.File == null)
                {
                    return BadRequest("Invalid post data or file is missing.");
                }

                using (var ms = new MemoryStream())
                {
                    value.File.CopyTo(ms);
                    value.Media = ms.ToArray(); // שמירת קובץ כ- byte[]
                }

                _postService.Add(value);
                return Ok("Post added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/Post/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingPost = _postService.Get(id);
                if (existingPost == null)
                {
                    return NotFound($"Post with ID {id} not found.");
                }

                _postService.Delete(id);
                return Ok("Post deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // עדכון פוסט קיים
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromForm] PostDto value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Invalid post data.");
                }

                var existingPost = _postService.Get(id);
                if (existingPost == null)
                {
                    return NotFound($"Post with ID {id} not found.");
                }

                // אם יש תמונה חדשה, עדכן אותה
                if (value.File != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        value.File.CopyTo(ms);
                        value.Media = ms.ToArray(); // עדכון התמונה בפורמט byte[]
                    }
                }
                else
                {
                    value.Media = existingPost.Media; // שמירת התמונה הקיימת
                }

                _postService.Update(value, id);
                return Ok("Post updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET api/Post/getPostImage/5
        [HttpGet("getPostImage/{id}")]
        public IActionResult GetImage(int id)
        {
            try
            {
                var post = _postService.Get(id);
                if (post == null || post.Media == null || post.Media.Length == 0)
                {
                    return NotFound("Post image not found.");
                }

                return File(post.Media, "image/jpeg"); // שליחת המידע הבינארי ישירות
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // הוספת לייק
        [Authorize]
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

        [Authorize]
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

        // קבלת פוסטים לפי ID של משתמש
        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { message = "Invalid user ID." });
                }

                var posts = _ExtentionPostService.GetByUserId(userId);
                if (posts == null || !posts.Any())
                {
                    return NotFound(new { message = "No posts found for this user." });
                }

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
