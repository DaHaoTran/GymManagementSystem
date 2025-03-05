using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Immutable;

namespace API.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly Branch_Int _branchSvc;
        public BranchesController(Branch_Int branchSvc)
        {
            _branchSvc = branchSvc;
        }

        /// <summary>
        /// Get branch list
        /// </summary>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>branch list</returns>
        [HttpGet]
        public async Task<IEnumerable<Branch>> GetBranchList([FromQuery] int limit)
        {
            return await _branchSvc.GetBranchList(limit);
        }

        /// <summary>
        /// Get the branch by branch code
        /// </summary>
        /// <param name="branch_code">branch code</param>
        /// <returns>a valid branch</returns>
        [HttpGet("{branch_code}")]
        public async Task<IActionResult> GetTheBranchByBranchCode(string branch_code)
        {
            if(string.IsNullOrEmpty(branch_code)) { return BadRequest(); }
            var getBranch = await _branchSvc.GetTheBranchByBranchCode(branch_code);
            if(getBranch == null) { return NotFound(); }
            return Ok(getBranch);
        }

        /// <summary>
        /// Get the branches by branch name/address
        /// </summary>
        /// <param name="str">search string</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid branch list</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTheBranchesBySearchString([FromQuery] string str, [FromQuery] int limit)
        {
            IEnumerable<Branch> getBranches;
            if (string.IsNullOrEmpty(str)) { return BadRequest(); }

            var byBranchName = await _branchSvc.GetTheBranchesByBranchName(str);
            getBranches = limit > 0 ? byBranchName.Take(limit) : byBranchName;

            if (getBranches.Count() >= limit) { return Ok(getBranches); }

            var byAddress = await _branchSvc.GetTheBranchesByAddress(str);
            getBranches = limit > 0 ? getBranches.Union(byAddress).Take(limit - getBranches.Count()) : getBranches.Union(byAddress);

            return Ok(getBranches);
        }

        /// <summary>
        /// Add a new branch
        /// </summary>
        /// <param name="branch">branch data</param>
        /// <returns>branch has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewBranch([FromBody] Branch branch)
        {
            if(branch == null) { return BadRequest(); }
            var newBranch = await _branchSvc.AddANewBranch(branch);
            return Ok(newBranch);
        }

        /// <summary>
        /// Edit an exist branch
        /// </summary>
        /// <param name="branch">branch data</param>
        /// <returns>branch has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistBranch([FromBody] Branch branch)
        {
            if(branch == null) { return BadRequest(); }
            var editBranch = await _branchSvc.EditAnExistBranch(branch);
            if(editBranch == null) { return NotFound(); }
            return Ok(editBranch);
        }

        /// <summary>
        /// Delete an exist branch
        /// </summary>
        /// <param name="branch_code">branch code</param>
        /// <returns>branch has deleted</returns>
        [HttpDelete("{branch_code}")] 
        public async Task<IActionResult> DeleteAnExistBranch(string branch_code)
        {
            if(string.IsNullOrEmpty(branch_code)) { return BadRequest(); }
            var delBranch = await _branchSvc.DeleteAnExistBranch(branch_code);
            if(delBranch == null) { return NotFound(); }
            return Ok(delBranch);
        }
    }
}
