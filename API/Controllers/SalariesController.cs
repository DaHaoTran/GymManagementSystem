using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/salaries")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly Salary_Int _salarySvc;
        public SalariesController(Salary_Int salarySvc)
        {
            _salarySvc = salarySvc;
        }

        /// <summary>
        /// Get salary list
        /// </summary>
        /// <param name="limit">data retrieval limit</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Salary>> GetSalaryList([FromQuery] int limit)
        {
            return await _salarySvc.GetSalaryList(limit);
        }

        /// <summary>
        /// Get the salary by salary code
        /// </summary>
        /// <param name="salary_code">salary code</param>
        /// <returns>a valid salary</returns>
        [HttpGet("{salary_code}")]
        public async Task<IActionResult> GetTheSalaryBySalaryCode(string salary_code)
        {
            if(string.IsNullOrEmpty(salary_code)) { return BadRequest(); }
            var getSalary = await _salarySvc.GetTheSalaryBySalaryCode(salary_code);
            if(getSalary == null) { return NotFound(); }
            return Ok(getSalary);
        }

        /// <summary>
        /// Add a new salary
        /// </summary>
        /// <param name="salary">salary data</param>
        /// <returns>salary has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewSalary([FromBody] Salary salary)
        {
            if(salary == null) { return BadRequest(); }
            var newSalary = await _salarySvc.AddANewSalary(salary);
            return Ok(newSalary);
        }

        /// <summary>
        /// Edit an exist salary
        /// </summary>
        /// <param name="salary">salary data</param>
        /// <returns>salary has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistSalary([FromBody] Salary salary)
        {
            if(salary == null) { return BadRequest(); }
            var editSalary = await _salarySvc.EditAnExistSalary(salary);
            if(editSalary == null) { return NotFound(); }
            return Ok(editSalary);
        }

        /// <summary>
        /// Delete an exist salary
        /// </summary>
        /// <param name="salary_code">salary code</param>
        /// <returns>salary has deleted</returns>
        [HttpDelete("{salary_code}")]
        public async Task<IActionResult> DeleteAnExistSalary(string salary_code)
        {
            if(string.IsNullOrEmpty(salary_code)) { return BadRequest(); }
            var delSalary = await _salarySvc.DeleteAnExistSalary(salary_code);
            if(delSalary == null) { return NotFound(); }
            return Ok(delSalary);
        }
    }
}
