using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    public class UserController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        //Bcrypt
        [HttpGet("{id}")]

        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUser(id);

                if (user == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                await _userRepository.CreateUser(user);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        //[HttpPost("Registration")]
        //public async Task<IActionResult> Registration([FromBody] Registration registration)
        //{
        //    try
        //    {
        //        UserRole role = UserRole.User;
        //        var customer = await _userRepository.IsItAnExistingMail(registration.Email);
        //        if (customer == null)
        //        {
        //            await _customerRepository.Registration(registration.Name, registration.Password, registration.Email, registration.Phone, registration.Birthday, role);
        //            var cust = await _customerRepository.GetCustomerByParams(registration.Name, registration.Password, registration.Email, registration.Phone, registration.Birthday);
        //            return Ok(cust);
        //        }
        //        else
        //        {
        //            return BadRequest("Пользователь с такой почтой уже существует");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return new BadRequestObjectResult(
        //            new
        //            {
        //                message = ex.Message
        //            }
        //            );
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                var dbUser = await _userRepository.GetUser(id);
                if (dbUser == null)
                {
                    return NotFound();
                }
                else
                {
                    await _userRepository.UpdateUser(id, user);

                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var dbUser = await _userRepository.GetUser(id);
                if (dbUser == null)
                {
                    return NotFound();
                }
                else
                {
                    await _userRepository.DeleteUser(id);
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
