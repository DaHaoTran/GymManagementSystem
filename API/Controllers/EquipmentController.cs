using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/equipment")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly Equipment_Int _equipmentSvc;
        public EquipmentController(Equipment_Int equipmentSvc)
        {
            _equipmentSvc = equipmentSvc;
        }

        /// <summary>
        /// Get equipment list
        /// </summary>
        /// <param name="limit">data retrieval data (0 will take all)</param>
        /// <returns>equipment list</returns>
        [HttpGet]
        public async Task<IEnumerable<Equipment>> GetEquipmentList([FromQuery] int limit)
        {
            var equipment = await _equipmentSvc.GetEquipmentList();
            if(limit <= 0) { return equipment; }
            return equipment.Take(limit);
        }

        /// <summary>
        /// Get the equipment by equip code
        /// </summary>
        /// <param name="equip_code">equipment code</param>
        /// <returns>a valid equipment</returns>
        [HttpGet("{equip_code}")]
        public async Task<IActionResult> GetTheEquipmentByEquipCode(string equip_code)
        {
            if(string.IsNullOrEmpty(equip_code)) { return BadRequest(); }
            var getEquipment = await _equipmentSvc.GetTheEquipmentByEquipCode(equip_code);
            if(getEquipment == null) { return NotFound(); }
            return Ok(getEquipment);
        }

        /// <summary>
        /// Get the equipment by equip name
        /// </summary>
        /// <param name="str">search string</param>
        /// <param name="limit">data retrieval data (0 will take all)</param>
        /// <returns>valid equipment list</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTheEquipmentBySearchString([FromQuery] string str, [FromQuery] int limit)
        {
            IEnumerable<Equipment> getEquipment;
            if(string.IsNullOrEmpty(str)) { return BadRequest(); }

            var byEquipName = await _equipmentSvc.GetTheEquipmentByEquipName(str);
            getEquipment = limit > 0 ? byEquipName.Take(limit) : byEquipName;

            return Ok(getEquipment);
        }

        /// <summary>
        /// Add a new equipment
        /// </summary>
        /// <param name="equipment">equipment data</param>
        /// <returns>equipment have added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewEquipment([FromBody] Equipment equipment)
        {
            if(equipment == null) { return BadRequest(); }
            var newEquipment = await _equipmentSvc.AddANewEquipment(equipment);
            return Ok(newEquipment);
        }

        /// <summary>
        /// Edit an exist equipment
        /// </summary>
        /// <param name="equipment">equipment data</param>
        /// <returns>equipment have edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistEquipment([FromBody] Equipment equipment)
        {
            if(equipment == null) { return BadRequest(); }
            var editEquipment = await _equipmentSvc.EditAnExistEquipment(equipment);
            if(editEquipment == null) { return NotFound(); }
            return Ok(editEquipment);
        }

        /// <summary>
        /// Delete an exist equipment
        /// </summary>
        /// <param name="equip_code">equipment code</param>
        /// <returns>equipment have deleted</returns>
        [HttpDelete("{equip_code}")]
        public async Task<IActionResult> DeleteAnExistEquipment(string equip_code)
        {
            if(string.IsNullOrEmpty(equip_code)) { return BadRequest(); }
            var delEquipment = await _equipmentSvc.DeleteAnExistEquipment(equip_code);
            if(delEquipment == null) { return NotFound(); }
            return Ok(delEquipment);
        }
    }
}
