using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Concurrent;
using Common.Dto;
using System.Text.Json;
using Service.Interfaces;

namespace SportNetworkServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private static ConcurrentDictionary<int, WebSocket> _usersSockets = new ConcurrentDictionary<int, WebSocket>();
        private readonly IService<ChatMessageDto> service;
        public ChatController(IService<ChatMessageDto> service) {
        this.service = service; 
        }
        [HttpGet("connect")]

        public async Task<IActionResult> Connect(int userId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _usersSockets.TryAdd(userId, webSocket);

                try
                {
                    await ReceiveMessages(webSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"שגיאה בעת קבלת הודעות: {ex.Message}");
                }
                finally
                {
                    _usersSockets.Remove(userId, out _);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "חיבור נסגר", CancellationToken.None);
                }

                return Ok();
            }
            else
            {
                return BadRequest("בקשה אינה חיבור WebSocket תקני");
            }
        }

        private async Task ReceiveMessages(WebSocket socket)
        {
            byte[] buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        await BroadcastMessage(message);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"שגיאה בקבלת הודעה: {ex.Message}");
                    break;
                }
            }
        }

        private async Task BroadcastMessage(string messageJson)
        {
            var buffer = Encoding.UTF8.GetBytes(messageJson);
            var msgObj = JsonSerializer.Deserialize<ChatMessageDto>(messageJson);
           
            

            if (msgObj.SentDate == default)
            {
                msgObj.SentDate = DateTime.UtcNow;
            }
             await SaveMessageToDatabase(msgObj);//save the message on data
            var senderSocket = _usersSockets[msgObj.SenderId];//get the sendrt to the dictinary

            if (msgObj == null || !_usersSockets.ContainsKey(msgObj.RecipientId))
            {
                await senderSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine("Recipient not connected or invalid message");
                return;
            } 
            var recipientSocket = _usersSockets[msgObj.RecipientId];//get the resipient from the ductinary
            
            if (recipientSocket.State == WebSocketState.Open)
            {
                await recipientSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await senderSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
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
    }
}