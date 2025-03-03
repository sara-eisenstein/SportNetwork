using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IService<ChallengeDto> _challengeService;
        private readonly IChallengeService _challengeService2;

        public ChallengeController(IService<ChallengeDto> service, IChallengeService challengeService2)
        {
            _challengeService = service;
            _challengeService2 = challengeService2;
        }

        [HttpGet]
        public List<ChallengeDto> Get()
        {
            return _challengeService.GetAll();
        }

        [HttpGet("{id}")]
        public ChallengeDto Get(int id)
        {
            return _challengeService.Get(id);
        }
        [HttpGet("/challengeToUser/")]
        public List<ChallengeDto> GetChallengeToUser(int userId)
        {
            return _challengeService2.GetChallengesByUserId(userId);
        }
        [HttpPost]
        public void Post([FromForm] ChallengeDto value)
        {
            _challengeService.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromForm] ChallengeDto value)
        {
            _challengeService.Update(value, id);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _challengeService.Delete(id);
        }
    }
}
