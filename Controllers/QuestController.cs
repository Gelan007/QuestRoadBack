using Microsoft.AspNetCore.Mvc;
using QuestRoadBack.Contracts;
using QuestRoadBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController: ControllerBase
    {
        private readonly IQuestRepository _questRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        private string role => User.Claims.Single(c => c.Type == "role").Value;
        public QuestController(IQuestRepository questRepository)
        {
            _questRepository = questRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetQuests()
        {
            try
            {   
                    var quests = await _questRepository.GetQuests();
                    return Ok(quests);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        //Bcrypt
        [HttpGet("{id}")]

        public async Task<IActionResult> GetQuest(int id)
        {
            try
            {
                var quest = await _questRepository.GetQuest(id);

                if (quest == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(quest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuest(Quest quest)
        {
            try
            {
                await _questRepository.CreateQuest(quest);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuest(int id, Quest quest)
        {
            try
            {
                var dbQuest = await _questRepository.GetQuest(id);
                if (dbQuest == null)
                {
                    return NotFound();
                }
                else
                {
                    await _questRepository.UpdateQuest(id, quest);

                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuest(int id)
        {
            try
            {
                var dbQuest = await _questRepository.GetQuest(id);
                if (dbQuest == null)
                {
                    return NotFound();
                }
                else
                {
                    await _questRepository.DeleteQuest(id);
                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("Popular")]
        public async Task<IActionResult> GetMostPopularQuestsAsync()
        {
            try
            {
                var quests = await _questRepository.GetMostPopularQuestsAsync();
                return Ok(quests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
