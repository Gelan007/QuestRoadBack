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
    public class CompanyController: ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepository.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }
        //Bcrypt
        [HttpGet("{id}")]

        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompany(id);

                if (company == null)
                {
                    return NotFound();
                }
                else
                {

                    return Ok(company);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany(Company company)
        {
            try
            {
                await _companyRepository.CreateCompany(company);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, Company company)
        {
            try
            {
                var dbCompany = await _companyRepository.GetCompany(id);
                if (dbCompany == null)
                {
                    return NotFound();
                }
                else
                {
                    await _companyRepository.UpdateCompany(id, company);

                    return Ok("Ok");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany = await _companyRepository.GetCompany(id);
                if (dbCompany == null)
                {
                    return NotFound();
                }
                else
                {
                    await _companyRepository.DeleteCompany(id);
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
