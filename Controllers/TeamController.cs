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
    public class TeamController: ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            try
            {
                var teams = await _teamRepository.GetTeams();
                return Ok(teams);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        //Bcrypt
        [HttpGet("{id}")]

        public async Task<IActionResult> GetTeam(int id)
        {
            try
            {
                var team = await _teamRepository.GetTeam(id);

                if (team == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(team);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam(Team team)
        {
            try
            {
                await _teamRepository.CreateTeam(team);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, Team team)
        {
            try
            {
                var dbTeam = await _teamRepository.GetTeam(id);
                if (dbTeam == null)
                {
                    return NotFound();
                }
                else
                {
                    await _teamRepository.UpdateTeam(id, team);

                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var dbTeam = await _teamRepository.GetTeam(id);
                if (dbTeam == null)
                {
                    return NotFound();
                }
                else
                {
                    await _teamRepository.DeleteTeam(id);
                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
