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
    public class BookingController: ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly Team _team;
        public BookingController(IBookingRepository bookingRepository, ITeamRepository teamRepository, IMemberRepository memberRepository)
        {
            _bookingRepository = bookingRepository;
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                var bookings = await _bookingRepository.GetBookings();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        //Bcrypt
        [HttpGet("{id}")]

        public async Task<IActionResult> GetBooking(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetBooking(id);

                if (booking == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(booking);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            try
            {
                await _bookingRepository.CreateBooking(booking);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, Booking booking)
        {
            try
            {
                var dbBooking = await _bookingRepository.GetBooking(id);
                if (dbBooking == null)
                {
                    return NotFound();
                }
                else
                {
                    await _bookingRepository.UpdateBooking(id, booking);

                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                var dbBooking = await _bookingRepository.GetBooking(id);
                if (dbBooking == null)
                {
                    return NotFound();
                }
                else
                {
                    await _bookingRepository.DeleteBooking(id);
                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> MakeSaleForTeam()
        {
            try
            {
                var teamId = await _teamRepository.GetTeamIdByPhoneAsync(_team.Phone);
                var countOfMembers = await _memberRepository.GetCountOfUsersByTeamIdAsync(teamId);

                double coefficient; 

            }
        }
    }
}
