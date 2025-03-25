using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;
using System.Security.Claims;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatMessageController : ControllerBase
    {
        private readonly IService<ChatMessageDto> _chatMessageService;
        private readonly IChatMessageService _customChatService;

        public ChatMessageController(IService<ChatMessageDto> service, IChatMessageService customChatService)
        {
            _chatMessageService = service;
            _customChatService = customChatService;
        }

        // שליחת הודעה חדשה
        [HttpPost]
        public IActionResult Post([FromBody] ChatMessageDto value)
        {
            try
            {
                if (value == null)
                    return BadRequest("Invalid chat message data.");

                value.SentDate = DateTime.UtcNow;
                _chatMessageService.Add(value);
                return Ok(value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //קבלת חמש הודעות ישנות 
        [HttpGet("GetChatMessages")]
        public IActionResult GetChatMessages([FromQuery] int userId, [FromQuery] int otherUserId, [FromQuery] int pageNumber = 1)
        {
            try
            {
                var messages = _customChatService.GetChatMessages(userId, otherUserId, pageNumber);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // עדכון הודעה
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ChatMessageDto value)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid chat message ID.");

                var existingMessage = _chatMessageService.Get(id);
                if (existingMessage == null)
                    return NotFound($"Chat message with ID {id} not found.");

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (existingMessage.SenderId.ToString() != userId)
                    return Forbid();

                _chatMessageService.Update(value, id);
                return Ok("Chat message updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // מחיקת הודעה
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid chat message ID.");

                var existingMessage = _chatMessageService.Get(id);
                if (existingMessage == null)
                    return NotFound($"Chat message with ID {id} not found.");

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (existingMessage.SenderId.ToString() != userId)
                    return Forbid();

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
