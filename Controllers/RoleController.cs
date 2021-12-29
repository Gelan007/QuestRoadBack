using Microsoft.AspNetCore.Mvc;
using QuestRoadBack.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestRoadBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController: ControllerBase
    {
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public RoleController(IHelpRepository helpRepository)
        {
            _helpRepository = helpRepository;
        }

        [HttpGet("Role")]
        public async Task<IActionResult> IsAdminAsync()
        {
            try
            {
                var role = await _helpRepository.IsAdminAsync(UserId);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
