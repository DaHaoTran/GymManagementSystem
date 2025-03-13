using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Authorize]
    [Route("api/working-checks")]
    [ApiController]
    public class WorkingChecksController : ControllerBase
    {
        private readonly WorkingCheck_Int _WKSvc;
        public WorkingChecksController(WorkingCheck_Int wKSvc)
        {
            _WKSvc = wKSvc;
        }

        /// <summary>
        /// Get working check list
        /// </summary>
        /// <param name="sort">sort type (asc or desc) (by CheckDate)</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>working check list</returns>
        [HttpGet]
        public async Task<IEnumerable<WorkingCheck>> GetWorkingCheckList([FromQuery] string sort, [FromQuery] int limit)
        {
            IEnumerable<WorkingCheck> workingChecks = await _WKSvc.GetWorkingCheckList(limit);

            switch(sort)
            {
                case "asc":
                    workingChecks = workingChecks.OrderBy(x => x.CheckDate);
                    break;
                case "desc":
                    workingChecks = workingChecks.OrderByDescending(x => x.CheckDate);
                    break;
                default:
                    break;
            }

            return workingChecks;
        }

        /// <summary>
        /// Get the working check by order number
        /// </summary>
        /// <param name="order_number">order number</param>
        /// <returns>a valid working check</returns>
        [HttpGet("{order_number}")]
        public async Task<IActionResult> GetTheWorkingCheckByOrderNumber(int order_number)
        {
            if(order_number <= 0) { return BadRequest(); }
            var getWK = await _WKSvc.GetTheWorkingCheckByOrderNumber(order_number);
            if(getWK == null) { return NotFound(); }
            return Ok(getWK);
        }

        /// <summary>
        /// Get the working checks by account code
        /// </summary>
        /// <param name="account_code">account code</param>
        /// <param name="sort">sort type (asc or desc) (by CheckDate)</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid working check list</returns>
        [HttpGet("working-checks/{account_code}/accounts/")]
        public async Task<IActionResult> GetTheWorkingChecksByAccountCode(string account_code, [FromQuery] string sort, [FromQuery] int limit)
        {
            if(string.IsNullOrEmpty(account_code)) { return BadRequest(); }
            IEnumerable<WorkingCheck> getWKs = await _WKSvc.GetTheWorkingChecksByAccountCode(account_code);

            if (limit > 0) { getWKs = getWKs.Take(limit); }

            switch (sort)
            {
                case "asc":
                    getWKs = getWKs.OrderBy(x => x.CheckDate);
                    break;
                case "desc":
                    getWKs = getWKs.OrderByDescending(x => x.CheckDate);
                    break;
                default:
                    break;
            }

            return Ok(getWKs);
        }

        /// <summary>
        /// Add a new working check
        /// </summary>
        /// <param name="workingCheck">working check data</param>
        /// <returns>working check has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewWorkingCheck([FromBody] WorkingCheck workingCheck)
        {
            if(workingCheck == null) { return BadRequest(); }
            var newWK = await _WKSvc.AddANewWorkingCheck(workingCheck);
            return Ok(newWK);
        }

        /// <summary>
        /// Edit an exist working check
        /// </summary>
        /// <param name="workingCheck">working check data</param>
        /// <returns>working check has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistWorkingCheck([FromBody] WorkingCheck workingCheck)
        {
            if(workingCheck == null) { return BadRequest(); }
            var editWK = await _WKSvc.EditAnExistWorkingCheck(workingCheck);
            if(editWK == null) { return NotFound(); }
            return Ok(editWK);
        }

        /// <summary>
        /// Delete an exist working check
        /// </summary>
        /// <param name="order_number">order number</param>
        /// <returns>working check has deleted</returns>
        [HttpDelete("{order_number}")]
        public async Task<IActionResult> DeleteAnExistWorkingCheck(int order_number)
        {
            if(order_number <= 0) { return BadRequest(); }
            var delWK = await _WKSvc.DeleteAnExistWorkingCheck(order_number);
            if(delWK == null) { return NotFound(); }
            return Ok(delWK);
        }

    }
}
