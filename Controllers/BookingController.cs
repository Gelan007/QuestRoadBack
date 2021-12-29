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

        private readonly IHelpRepository _helpRepository;
        private readonly IQuestRepository _questRepository;
        
        public BookingController(IBookingRepository bookingRepository, ITeamRepository teamRepository, IMemberRepository memberRepository, IHelpRepository helpRepository, IQuestRepository questRepository)
        {
            _bookingRepository = bookingRepository;
            _memberRepository = memberRepository;
            _teamRepository = teamRepository;
            _helpRepository = helpRepository;
            _questRepository = questRepository;

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
                await _bookingRepository.CreateBooking(booking.Quest_id,booking.Team_id,booking.Price,booking.Time,booking.Description);
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
        
        [HttpPost("Form")]
        public async Task<IActionResult> CreateBookingAsync(BookingConstructor bookingConstructor)
        {
            try
            {
                
                var quest = await _questRepository.GetQuest(bookingConstructor.Quest_id); 

                // 1 - получим телефон командира 
                var cap = await _helpRepository.GetPhoneByIdAsync(bookingConstructor.User_id);


                if(cap == null || quest == null)
                {
                    return NotFound("Что-то пошло не так");
                }
                if (quest.Max_count_users < bookingConstructor.CountOfUsers || bookingConstructor.CountOfUsers <= 0)
                {
                    return BadRequest("Недопустимое количество человек в команде");
                }


                // - создание команды 
                await _teamRepository.CreateTeamFromBookingAsync(bookingConstructor.TeamName, bookingConstructor.CountOfUsers, cap.Phone);
                // получить айди команды
                var team = await _teamRepository.GetTeamByPhoneAndNameAsync(bookingConstructor.TeamName, cap.Phone);
                if(team == null)
                {
                    return NotFound("Что-то полшло не так");
                }
                DateTime today = DateTime.Now;

                //создать мембера
                await _memberRepository.CreateMemberAsync(bookingConstructor.User_id, team.Team_id, today);



                await _bookingRepository.CreateBooking(bookingConstructor.Quest_id, team.Team_id, quest.Price, bookingConstructor.Date, bookingConstructor.Description);
                return Ok("Ok");

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
