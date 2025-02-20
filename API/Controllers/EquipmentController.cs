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
        /// <returns>equipment list</returns>
        [HttpGet]
        public async Task<List<Equipment>> GetEquipmentList()
        {
            return await _equipmentSvc.GetEquipmentList();
        }

        /// <summary>
        /// Get the equipment by equip code
        /// </summary>
        /// <param name="equip_code">equipment code</param>
        /// <returns>a valid equipment</returns>
        [HttpGet("equip-code/{equip_code}")]
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
        /// <param name="equip_name">equipment name</param>
        /// <returns>valid equipment list</returns>
        [HttpGet("equip-name/{equip_name}")]
        public async Task<IActionResult> GetTheEquipmentByEquipName(string equip_name)
        {
            if(string.IsNullOrEmpty(equip_name)) { return BadRequest(); }
            var getEquipment = await _equipmentSvc.GetTheEquipmentByEquipName(equip_name);
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
