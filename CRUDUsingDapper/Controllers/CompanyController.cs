using CRUDUsingDapper.Model;
using CRUDUsingDapper.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDUsingDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repo;
        public CompanyController(ICompanyRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var comapanies = await _repo.GetAllComapnies();
                return Ok(comapanies);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            try
            {
                var company = await _repo.GetCompanyById(id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertCompany(InsertCompany insertCompany)
        {
            try
            {
                await _repo.InsertCompany(insertCompany);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, UpdateCompany updateCompany)
        {
            try
            {
                await _repo.UpdateCompany(id, updateCompany);
                return Ok();
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
                await _repo.DeleteCompany(id);
                return StatusCode(200);
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
