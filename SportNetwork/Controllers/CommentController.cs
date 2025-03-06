using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
<<<<<<< HEAD
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
=======
using Service.Services;
using System.Security.Claims;
>>>>>>> 373b4ec11967f64dd9046533683847848a5db4fe

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IService<CommentDto> _commentservice;
        private readonly IcommentService _extensionCommentService;

        public CommentController(IService<CommentDto> commentservice, IcommentService extensionCommentService)
        {
            _commentservice = commentservice;
            _extensionCommentService = extensionCommentService;
        }

<<<<<<< HEAD
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
=======
>>>>>>> 373b4ec11967f64dd9046533683847848a5db4fe
        // GET api/<CommentController>/5
        [HttpGet("/getcommentByPostId/")]
        public IActionResult GetCommentByPostId(int postId)
        {
            try
            {
                if (postId <= 0)
                {
                    return BadRequest("Invalid post ID.");
                }

                var comments = _extensionCommentService.GetCommentByPostId(postId);
                if (comments == null || !comments.Any())
                {
                    return NotFound($"No comments found for post ID {postId}.");
                }

                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        // POST api/<CommentController>
        [HttpPost]
<<<<<<< HEAD
        [Authorize]
        public void Post([FromForm] CommentDto value)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != value.UserId.ToString())
                _commentservice.Add(value);
            else
                throw new Exception("not connect");
=======
        public IActionResult Post([FromForm] CommentDto value)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated.");
                }


                if (value == null)
                {
                    return BadRequest("Invalid comment data.");
                }

                _commentservice.Add(value);
                return Ok("Comment added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
>>>>>>> 373b4ec11967f64dd9046533683847848a5db4fe
        }

        [Authorize]
        [HttpPut("{id}")]
<<<<<<< HEAD
        [Authorize]
        public void Put(int id, [FromForm] CommentDto value)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != value.UserId.ToString())
                _commentservice.Update(value, id);
            else throw new Exception("user not conncet");
=======
        public IActionResult Put(int id, [FromForm] CommentDto value)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated.");
                }

                if (id <= 0)
                {
                    return BadRequest("Invalid comment ID.");
                }

                var existingComment = _commentservice.Get(id);
                if (existingComment == null)
                {
                    return NotFound($"Comment with ID {id} not found.");
                }

                // 🔴 בדיקה: האם המשתמש המחובר הוא זה שכתב את התגובה?
                if (existingComment.UserId.ToString() != userId)
                {
                    return Forbid(); // ⛔ חסימת גישה אם המשתמש אינו היוצר
                }

                _commentservice.Update(value, id);
                return Ok("Comment updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
>>>>>>> 373b4ec11967f64dd9046533683847848a5db4fe
        }

        [Authorize]
        [HttpDelete("{id}")]
<<<<<<< HEAD
        [Authorize]
        public void Delete(int id)
        {
            var value=_commentservice.Get(id);
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value != value.UserId.ToString())
                _commentservice.Delete(id);
            else throw new Exception("user not connect");
=======
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User is not authenticated.");
                    }

                if (id <= 0)
                {
                    return BadRequest("Invalid comment ID.");
                }

                var existingComment = _commentservice.Get(id);
                if (existingComment == null)
                {
                    return NotFound($"Comment with ID {id} not found.");
                }

                // 🔴 בדיקה: האם המשתמש המחובר הוא זה שכתב את התגובה?
                if (existingComment.UserId.ToString() != userId)
                {
                    return Forbid(); // ⛔ חסימת גישה אם המשתמש אינו היוצר
                }

                _commentservice.Delete(id);
                return Ok("Comment deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
>>>>>>> 373b4ec11967f64dd9046533683847848a5db4fe
        }

    }
}
