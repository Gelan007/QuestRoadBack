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
    public class MemberController: ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IBookingRepository _bookingRepository;
        public MemberController(IMemberRepository memberRepository, IBookingRepository bookingRepository)
        {
            _memberRepository = memberRepository;
            _bookingRepository = bookingRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                var members = await _memberRepository.GetMembers();
                return Ok(members);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateMember(Member member)
        {
            try
            {
                await _memberRepository.CreateMember(member);

                int count = await _memberRepository.GetCountOfUsersByTeamIdAsync(member.Team_id);
                double coef = 1;
                if(count >= 3)
                {
                    switch (count)
                    {
                        case 3:
                            coef = 0.95;
                            break;
                        case 4:
                            coef = 0.90;
                            break;
                        default:
                            coef = 0.85;
                            break;
                    }

                    await _bookingRepository.UpdateBookingPriceAsync(member.Team_id, coef);
                }
                
                       
                     
                     


                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            { 
                 await _memberRepository.DeleteMember(id);
                 return Ok("Ok");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
