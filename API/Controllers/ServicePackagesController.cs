using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/service-packages")]
    [ApiController]
    public class ServicePackagesController : ControllerBase
    {
        private readonly ServicePackage_Int _sPSvc;
        public ServicePackagesController(ServicePackage_Int sPSvc)
        {
            _sPSvc = sPSvc;
        }

        /// <summary>
        /// Get service package list
        /// </summary>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>service package list</returns>
        [HttpGet]
        public async Task<IEnumerable<ServicePackage>> GetServicePackageList([FromQuery] int limit)
        {
            var sPs = await _sPSvc.GetServicePackageList();
            if(limit <= 0) { return sPs; }
            return sPs.Take(limit);
        }

        /// <summary>
        /// Get the service package by package code
        /// </summary>
        /// <param name="package_code">package code</param>
        /// <returns>a valid service package</returns>
        [HttpGet("{package_code}")]
        public async Task<IActionResult> GetTheServicePackageByPackageCode(string package_code)
        {
            if(string.IsNullOrEmpty(package_code)) { return BadRequest(); }
            var getSP = await _sPSvc.GetTheServicePackageByPackageCode(package_code);
            if(getSP == null) { return NotFound(); }
            return Ok(getSP);
        }

        /// <summary>
        /// Get the service package by package name
        /// </summary>
        /// <param name="str">search string</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid service package list</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTheServicePackagesBySearchString([FromQuery] string str, [FromQuery] int limit)
        {
            if(string.IsNullOrEmpty(str)) { return BadRequest(); }
            var getSPs = await _sPSvc.GetTheServicePackagesByPackageName(str);
            if(limit <= 0) { return Ok(getSPs); }
            return Ok(getSPs.Take(limit));
        }

        /// <summary>
        /// Add a new service package
        /// </summary>
        /// <param name="servicePackage">service package data</param>
        /// <returns>service package has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewServicePackage([FromBody] ServicePackage servicePackage)
        {
            if(servicePackage == null) { return BadRequest(); }
            var newSP = await _sPSvc.AddANewServicePackage(servicePackage);
            return Ok(newSP);
        }

        /// <summary>
        /// Edit an exist service package
        /// </summary>
        /// <param name="servicePackage">service package data</param>
        /// <returns>service package has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistServicePackage([FromBody] ServicePackage servicePackage)
        {
            if(servicePackage == null) { return BadRequest(); }
            var editSP = await _sPSvc.EditAnExistServicePackage(servicePackage);
            if(editSP == null) { return NotFound(); }
            return Ok(editSP);
        }

        /// <summary>
        /// Delete an exist service package 
        /// </summary>
        /// <param name="package_code">package code</param>
        /// <returns>service package has deleted</returns>
        [HttpDelete("{package_code}")]
        public async Task<IActionResult> DeleteAnExistServicePackage(string package_code)
        {
            if(string.IsNullOrEmpty(package_code)) { return BadRequest(); }
            var delSP = await _sPSvc.DeleteAnExistServicePackage(package_code);
            if(delSP == null) { return NotFound(); }
            return Ok(delSP);
        }
    }
}
