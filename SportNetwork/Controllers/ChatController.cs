using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportNetworkServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private static List<WebSocket> _clients = new List<WebSocket>();

        [HttpGet("connect")]
     
        public async Task<IActionResult> Connect()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _clients.Add(webSocket);

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
                    _clients.Remove(webSocket);
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

        private async Task BroadcastMessage(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var tasks = _clients
                .Where(client => client.State == WebSocketState.Open)
                .Select(client =>
                    client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None))
                .ToList();

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה בשליחת הודעה: {ex.Message}");
            }
        }
    }
}
