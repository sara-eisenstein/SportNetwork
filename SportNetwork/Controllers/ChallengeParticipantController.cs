using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeParticipantController : ControllerBase
    {

        private readonly IService<ChallengeParticipantDto> _challengeParticipantService;
        public ChallengeParticipantController(IService<ChallengeParticipantDto> service)
        {
            this._challengeParticipantService = service;

        }

        // GET: api/<ChallengeParticipantController>
        [HttpGet]
        public List<ChallengeParticipantDto> Get()
        {
            return _challengeParticipantService.GetAll();
        }

        // GET api/<ChallengeParticipantController>/5
        [HttpGet("{id}")]
        public ChallengeParticipantDto Get(int id)
        {
            return _challengeParticipantService.Get(id);
        }

        // POST api/<ChallengeParticipantController>
        [HttpPost]
        public void Post([FromForm] ChallengeParticipantDto value)
        {
            _challengeParticipantService.Add(value);
        }

        // PUT api/<ChallengeParticipantController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm ] ChallengeParticipantDto value)
        {
            _challengeParticipantService.Update(value, id   );

        }

        // DELETE api/<ChallengeParticipantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _challengeParticipantService.Delete(id);
        }
    }
}
