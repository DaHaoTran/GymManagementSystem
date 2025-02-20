using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

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
        /// <returns> Branch list </returns>
        [HttpGet]
        public async Task<List<Branch>> GetBranchList()
        {
            return await _branchSvc.GetBranchList();
        }

        /// <summary>
        /// Get the branch by branch code
        /// </summary>
        /// <param name="branch_code">branch code</param>
        /// <returns>a valid branch</returns>
        [HttpGet("branch-code/{branch_code}")]
        public async Task<IActionResult> GetTheBranchByBranchCode(string branch_code)
        {
            if(string.IsNullOrEmpty(branch_code)) { return BadRequest(); }
            var getBranch = await _branchSvc.GetTheBranchByBranchCode(branch_code);
            if(getBranch == null) { return NotFound(); }
            return Ok(getBranch);
        }

        /// <summary>
        /// Get the branches by branch name
        /// </summary>
        /// <param name="branch_name">branch name</param>
        /// <returns>valid branch list</returns>
        [HttpGet("branch-name/{branch_name}")]
        public async Task<IActionResult> GetTheBranchesByBranchName(string branch_name)
        {
            if (string.IsNullOrEmpty(branch_name)) { return BadRequest(); }
            var getBranches = await _branchSvc.GetTheBranchesByBranchName(branch_name);
            return Ok(getBranches);
        }

        /// <summary>
        /// Get the branches by address
        /// </summary>
        /// <param name="address">address</param>
        /// <returns>valid branch list</returns>
        [HttpGet("address/{address}")]
        public async Task<IActionResult> GetTheBranchesByAddress(string address)
        {
            if(string.IsNullOrEmpty(address)) { return BadRequest(); }
            var getBranches = await _branchSvc.GetTheBranchesByAddress(address);
            return Ok(getBranches);
        }

        /// <summary>
        /// Add a new branch
        /// </summary>
        /// <param name="branch">branch data</param>
        /// <returns>branch have added</returns>
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
        /// <returns>branch have edited</returns>
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
        /// <returns>branch have deleted</returns>
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
