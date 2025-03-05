using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IService<ChatMessageDto> _chatMessageService;

        public ChatMessageController(IService<ChatMessageDto> service)
        {
            _chatMessageService = service;
        }

        [HttpPost]
        public IActionResult Post([FromForm] ChatMessageDto value)
        {
            try
            {
                if (value == null)
                {
                    return BadRequest("Invalid chat message data.");
                }

                _chatMessageService.Add(value);
                return Ok("Chat message added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] ChatMessageDto value)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid chat message ID.");
                }

                var existingMessage = _chatMessageService.Get(id);
                if (existingMessage == null)
                {
                    return NotFound($"Chat message with ID {id} not found.");
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (existingMessage.SenderId.ToString() != userId)
                {
                    return Forbid(); // ⛔ המשתמש אינו הבעלים של ההודעה
                }

                _chatMessageService.Update(value, id);
                return Ok("Chat message updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                

                if (id <= 0)
                {
                    return BadRequest("Invalid chat message ID.");
                }

                var existingMessage = _chatMessageService.Get(id);
                if (existingMessage == null)
                {
                    return NotFound($"Chat message with ID {id} not found.");
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (existingMessage.SenderId.ToString() != userId)
                {
                    return Forbid(); // ⛔ המשתמש אינו הבעלים של ההודעה
                }

                _chatMessageService.Delete(id);
                return Ok("Chat message deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
