using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

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

        [HttpGet]
        public List<ChatMessageDto> Get()
        {
            return _chatMessageService.GetAll();
        }

        [HttpGet("{id}")]
        public ChatMessageDto Get(int id)
        {
            return _chatMessageService.Get(id);
        }

        [HttpPost]
        public void Post([FromForm] ChatMessageDto value)
        {
            _chatMessageService.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromForm] ChatMessageDto value)
        {
            _chatMessageService.Update(value, id);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _chatMessageService.Delete(id);
        }
    }
}
