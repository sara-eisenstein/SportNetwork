using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IService<AchievementDto> _achievementService;
        public AchievementController(IService<AchievementDto> service)
        {
            this._achievementService = service;

        }


        // GET: api/<AchievementController>
        [HttpGet]
        public List<AchievementDto> Get()
        {
            return _achievementService.GetAll();
        }

        // GET api/<AchievementController>/5
        [HttpGet("{id}")]
        public AchievementDto Get(int id)
        {
            return _achievementService.Get(id);
        }

        // POST api/<AchievementController>
        [HttpPost]
        public void Post([FromBody] AchievementDto value)
        {
            _achievementService.Add(value);
        }

        // PUT api/<AchievementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] AchievementDto value)
        {
            _achievementService.Update(value);
        }

        // DELETE api/<AchievementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _achievementService.Delete(id);
        }
    }
}
