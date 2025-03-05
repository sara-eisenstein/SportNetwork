using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

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

                var comments = _commentservice2.GetCommentByPostId(postId);
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

        // POST api/<CommentController>
        [HttpPost]
        public IActionResult Post([FromForm] CommentDto value)
        {
            try
            {
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
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] CommentDto value)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid comment ID.");
                }

                var existingComment = _commentservice.Get(id);
                if (existingComment == null)
                {
                    return NotFound($"Comment with ID {id} not found.");
                }

                _commentservice.Update(value, id);
                return Ok("Comment updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid comment ID.");
                }

                var existingComment = _commentservice.Get(id);
                if (existingComment == null)
                {
                    return NotFound($"Comment with ID {id} not found.");
                }

                _commentservice.Delete(id);
                return Ok("Comment deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
