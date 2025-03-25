using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto;
using Service.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private static ConcurrentDictionary<int, WebSocket> _usersSockets = new ConcurrentDictionary<int, WebSocket>();
        private readonly IChatMessageService _chatMessageService;
        private readonly IService<ChatMessageDto> service;

        public ChatController(IChatMessageService chatMessageService, IService<ChatMessageDto> service)
        {
            _chatMessageService = chatMessageService;
            this.service = service;
        }

        [HttpGet("connect")]
        public async Task Connect(int userId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                _usersSockets.TryAdd(userId, webSocket);

                //if (!_usersSockets.TryAdd(userId, webSocket))
                //{
                //    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "User already connected", CancellationToken.None);
                //     return;
                //}

                await ReceiveMessages(webSocket, userId);
                //_usersSockets.TryRemove(userId, out _);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        private async Task ReceiveMessages(WebSocket webSocket, int userId)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Console.WriteLine($"User {userId} disconnected.");
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                    break;
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received from {userId}: {message}");

                    await ForwardMessageToRecipient(message, userId);
                }
            }
        }

        private async Task ForwardMessageToRecipient(string messageJson, int senderId)
        {
            var msgObj = JsonSerializer.Deserialize<ChatMessageDto>(messageJson);
            if (msgObj == null || !_usersSockets.ContainsKey(msgObj.RecipientId))
            {
                Console.WriteLine("Recipient not connected or invalid message");
                return;
            }

            if (msgObj.SentDate == default)
            {
                msgObj.SentDate = DateTime.UtcNow;
            }

            // שמור את ההודעה ב-DB
            //await SaveMessageToDatabase(msgObj);

            var recipientSocket = _usersSockets[msgObj.RecipientId];
            if (recipientSocket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msgObj));
                await recipientSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task SaveMessageToDatabase(ChatMessageDto message)
        {
            try
            {
                // שמור את ההודעה ב-DB
                ChatMessageDto value = service.Add(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving message to DB: {ex.Message}");
            }
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto message)
        {
            if (_usersSockets.ContainsKey(message.RecipientId))
            {
                var socket = _usersSockets[message.RecipientId];

                if (message.SentDate == default)
                {
                    message.SentDate = DateTime.UtcNow;
                }

                var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                //await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await socket.SendAsync(
    new ArraySegment<byte>(Encoding.UTF8.GetBytes("{\"text\":\"Hello from server\"}")),
    WebSocketMessageType.Text,
    true,
    CancellationToken.None
);

                return Ok("Message sent.");
            }
            else
            {
                return BadRequest("Recipient not connected.");
            }
        }
    }
}