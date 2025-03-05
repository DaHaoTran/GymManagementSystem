using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly Role_Int _roleSvc;
        public RolesController(Role_Int roleSvc)
        {
            _roleSvc = roleSvc;
        }

        /// <summary>
        /// Get role list
        /// </summary>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>role list</returns>
        [HttpGet]
        public async Task<IEnumerable<Role>> GetRoleList([FromQuery] int limit)
        {
            return await _roleSvc.GetRoleList(limit);
        }

        /// <summary>
        /// Get the role by order number
        /// </summary>
        /// <param name="order_number">order number</param>
        /// <returns>a valid role</returns>
        [HttpGet("{order_number}")]
        public async Task<IActionResult> GetTheRoleByPackageCode(int order_number)
        {
            if (order_number <= 0) { return BadRequest(); }
            var getRole = await _roleSvc.GetTheRoleByOrderNumber(order_number);
            if (getRole == null) { return NotFound(); }
            return Ok(getRole);
        }

        /// <summary>
        /// Get the role by role name
        /// </summary>
        /// <param name="str">search string</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid role list</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTheRolesBySearchString([FromQuery] string str, [FromQuery] int limit)
        {
            if (string.IsNullOrEmpty(str)) { return BadRequest(); }
            var getRoles = await _roleSvc.GetTheRoleByRoleName(str);
            if (limit <= 0) { return Ok(getRoles); }
            return Ok(getRoles.Take(limit));
        }

        /// <summary>
        /// Add a new role
        /// </summary>
        /// <param name="role">role data</param>
        /// <returns>role has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewRole([FromBody] Role role)
        {
            if (role == null) { return BadRequest(); }
            var newRole = await _roleSvc.AddANewRole(role);
            return Ok(newRole);
        }

        /// <summary>
        /// Edit an exist role
        /// </summary>
        /// <param name="role">role data</param>
        /// <returns>role has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistRole([FromBody] Role role)
        {
            if (role == null) { return BadRequest(); }
            var editRole = await _roleSvc.EditAnExistRole(role);
            if (editRole == null) { return NotFound(); }
            return Ok(editRole);
        }

        /// <summary>
        /// Delete an exist role
        /// </summary>
        /// <param name="order_number">order number</param>
        /// <returns>role has deleted</returns>
        [HttpDelete("{order_number}")]
        public async Task<IActionResult> DeleteAnExistRole(int order_number)
        {
            if (order_number <= 0) { return BadRequest(); }
            var delRole = await _roleSvc.DeleteAnExistRole(order_number);
            if (delRole == null) { return NotFound(); }
            return Ok(delRole);
        }
    }
}
