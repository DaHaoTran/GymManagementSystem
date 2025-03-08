using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/fines")]
    [ApiController]
    public class FinesController : ControllerBase
    {
        private readonly Fine_Int _fineSvc;
        public FinesController(Fine_Int fineSvc)
        {
            _fineSvc = fineSvc;
        }

        /// <summary>
        /// Get fine list
        /// </summary>
        /// <param name="sort">sort type (asc or desc) (by Date)</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>fine list</returns>
        [HttpGet]
        public async Task<IEnumerable<Fine>> GetFineList([FromQuery] string sort, [FromQuery] int limit)
        {
            IEnumerable<Fine> fines = await _fineSvc.GetFineList(limit);

            switch(sort)
            {
                case "asc":
                    fines = fines.OrderBy(x => x.Date);
                    break;
                case "desc":
                    fines = fines.OrderByDescending(x => x.Date);
                    break;
                default:
                    break;
            }

            return fines;
        }

        /// <summary>
        /// Get the fine by fine code
        /// </summary>
        /// <param name="fine_code">fine code</param>
        /// <returns>a valid fine</returns>
        [HttpGet("{fine_code}")]
        public async Task<IActionResult> GetTheFineByFineCode(Guid fine_code)
        {
            var getFine = await _fineSvc.GetTheFineByFineCode(fine_code);
            if(getFine == null) { return NotFound(); }
            return Ok(getFine);
        }

        /// <summary>
        /// Get the fines by customer code
        /// </summary>
        /// <param name="customer_code">customer code</param>
        /// <param name="sort">sort type (asc or desc) (by Date)</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid customer list</returns>
        [HttpGet("{customer_code}/customers")]
        public async Task<IActionResult> GetTheFinesByCustomerCode(string customer_code, [FromQuery] string sort, [FromQuery] int limit)
        {
            if(string.IsNullOrEmpty(customer_code)) { return BadRequest(); }
            IEnumerable<Fine> getFines = await _fineSvc.GetTheFinesByCustomerCode(customer_code);
            if(limit > 0) { getFines = getFines.Take(limit); }

            switch(sort)
            {
                case "asc":
                    getFines = getFines.OrderBy(x => x.Date);
                    break;
                case "desc":
                    getFines = getFines.OrderByDescending(x => x.Date);
                    break;
                default:
                    break; 
            }

            return Ok(getFines);
        }

        /// <summary>
        /// Add a new fine
        /// </summary>
        /// <param name="fine">fine data</param>
        /// <returns>fine has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewFine([FromBody] Fine fine)
        {
            if(fine == null) { return BadRequest(); }
            var newFine = await _fineSvc.AddANewFine(fine);
            return Ok(newFine);
        }

        /// <summary>
        /// Edit an exist fine
        /// </summary>
        /// <param name="fine">fine data</param>
        /// <returns>fine has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistFine([FromBody] Fine fine)
        {
            if(fine == null) { return BadRequest(); }
            var editFine = await _fineSvc.EditAnExistFine(fine);
            if(editFine == null) { return NotFound(); }
            return Ok(editFine);
        }

        /// <summary>
        /// Delete an exist fine
        /// </summary>
        /// <param name="fine_code">fine code</param>
        /// <returns>fine has deleted</returns>
        [HttpDelete("{fine_code}")]
        public async Task<IActionResult> DeleteAnExistFine(Guid fine_code)
        {
            var delFine = await _fineSvc.DeleteAnExistFine(fine_code);
            if(delFine == null) { return NotFound(); }
            return Ok(delFine);
        }
    }
}
