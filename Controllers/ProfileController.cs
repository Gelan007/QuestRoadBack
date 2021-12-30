using Microsoft.AspNetCore.Http;
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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        [HttpGet("UserInfo")]
        public async Task<IActionResult> GetUserInfoAsync()
        {
            try
            {
                var user = await _profileRepository.GetUserInfoAsync(UserId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("UserBookings")]
        public async Task<IActionResult> GetUserBookingsAsync()
        {
            try
            {
                var bookings = await _profileRepository.GetUsersBookingsAsync(UserId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
