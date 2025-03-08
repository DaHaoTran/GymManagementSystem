using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/employee-salaries")]
    [ApiController]
    public class EmployeeSalariesController : ControllerBase
    {
        private readonly EmployeeSalary_Int _ESSvc;
        public EmployeeSalariesController(EmployeeSalary_Int eSSvc)
        {
            _ESSvc = eSSvc;
        }

        /// <summary>
        /// Get the employee salary list
        /// </summary>
        /// <param name="sort">sort type (asc or desc) (by Month)</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>employee salary list</returns>
        [HttpGet]
        public async Task<IEnumerable<EmployeeSalary>> GetEmployeeSalaryList([FromQuery] string sort, [FromQuery] int limit)
        {
            IEnumerable<EmployeeSalary> ESs = await _ESSvc.GetEmployeeSalaryList(limit);

            switch(sort)
            {
                case "asc":
                    ESs = ESs.OrderBy(x => x.Month);
                    break;
                case "desc":
                    ESs = ESs.OrderByDescending(x => x.Month);
                    break;
                default:
                    break;
            }

            return ESs;
        }

        /// <summary>
        /// Get the employee salary by employee salary code
        /// </summary>
        /// <param name="empsal_code">employee salary code</param>
        /// <returns>a valid employee salary</returns>
        [HttpGet("{empsal_code}")]
        public async Task<IActionResult> GetTheEmployeeSalaryByEmployeeSalaryCode(Guid empsal_code)
        {
            var getES = await _ESSvc.GetTheEmployeeSalaryByEmployeeSalaryCode(empsal_code);
            if(getES == null) { return NotFound(); }
            return Ok(getES);
        }

        /// <summary>
        /// Get the employee salaries by full name/branch name
        /// </summary>
        /// <param name="str">search string</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid employee salary list</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTheEmployeeSalariesBySearchString([FromQuery] string str, [FromQuery] int limit)
        {
            IEnumerable<EmployeeSalary> getESs;
            if(string.IsNullOrEmpty(str)) { return BadRequest(); }

            var byFullName = await _ESSvc.GetTheEmployeeSalariesByFullName(str);
            getESs = limit > 0 ? byFullName.Take(limit) : byFullName;

            if(getESs.Count() >= limit) { return Ok(getESs); }

            var byBranchName = await _ESSvc.GetTheEmployeeSalariesByBranchName(str);
            getESs = limit > 0 ? getESs.Union(byBranchName).Take(limit - getESs.Count()) : getESs.Union(byBranchName);

            return Ok(getESs);
        }

        /// <summary>
        /// Get the employee salaries by account code
        /// </summary>
        /// <param name="account_code">account code</param>
        /// <param name="sort">sort type (asc or desc) (by Month)</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid employee salary list</returns>
        [HttpGet("{account_code}/accounts")]
        public async Task<IActionResult> GetTheEmployeeSalariesByAccountCode(string account_code, [FromQuery] string sort, [FromQuery] int limit)
        {
            if(string.IsNullOrEmpty(account_code)) { return BadRequest(); }
            IEnumerable<EmployeeSalary> getESs = await _ESSvc.GetTheEmployeeSalariesByAccountCode(account_code);
            
            if(limit > 0) { getESs = getESs.Take(limit); }

            switch (sort)
            {
                case "asc":
                    getESs = getESs.OrderBy(x => x.Month);
                    break;
                case "desc":
                    getESs = getESs.OrderByDescending(x => x.Month);
                    break;
                default:
                    break;
            }

            return Ok(getESs);
        }

        /// <summary>
        /// Get the employee salaries by month
        /// </summary>
        /// <param name="month">month</param>
        /// <param name="year">year</param>
        /// <returns>valid employee salaries list</returns>
        [HttpGet("filter2")]
        public async Task<IActionResult> GetTheEmployeeSalariesByMonth([FromQuery] int month, [FromQuery] int year)
        {
            if(month <= 0) { return BadRequest(); }
            if(year <= 1999) { return BadRequest(); }
            var getESs = await _ESSvc.GetTheEmployeeSalariesByMonth(month, year);
            return Ok(getESs);
        }

        /// <summary>
        /// Add a new employee salary
        /// </summary>
        /// <param name="employeeSalary">employee salary data</param>
        /// <returns>employee salary has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewEmployeeSalary([FromBody] EmployeeSalary employeeSalary)
        {
            if(employeeSalary == null) { return BadRequest(); }
            var newES = await _ESSvc.AddANewEmployeeSalary(employeeSalary);
            return Ok(newES);
        }

        /// <summary>
        /// Edit an exist employee salary
        /// </summary>
        /// <param name="employeeSalary">employee salary data</param>
        /// <returns>employee salary has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistEmployeeSalary([FromBody] EmployeeSalary employeeSalary)
        {
            if(employeeSalary == null) { return BadRequest(); }
            var editES = await _ESSvc.EditAnExistEmployeeSalary(employeeSalary);
            if(editES == null) { return NotFound(); }
            return Ok(editES);
        }

        /// <summary>
        /// Delete an exist employee salary
        /// </summary>
        /// <param name="empsal_code">employee salary code</param>
        /// <returns>employee salary has deleted</returns>
        [HttpDelete("{empsal_code}")]
        public async Task<IActionResult> DeleteAnExistEmployeeSalary(Guid empsal_code)
        {
            var delES = await _ESSvc.DeleteAnExistEmployeeSalary(empsal_code);
            if(delES == null) { return NotFound(); }
            return Ok(delES);
        }
    }
}
