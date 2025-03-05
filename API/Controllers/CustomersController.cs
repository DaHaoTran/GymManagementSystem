using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Customer_Int _customerSvc;
        public CustomersController(Customer_Int customerSvc)
        {
            _customerSvc = customerSvc;
        }

        /// <summary>
        /// Get customer list
        /// </summary>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>customer list</returns>
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCustomerList([FromQuery] int limit)
        {
            return await _customerSvc.GetCustomerList(limit);
        }

        /// <summary>
        /// Get the customer by customer code
        /// </summary>
        /// <param name="customer_code">customer code</param>
        /// <returns>a valid customer</returns>
        [HttpGet("{customer_code}")]
        public async Task<IActionResult> GetTheCustomerByCustomerCode(string customer_code)
        {
            if(string.IsNullOrEmpty(customer_code)) { return BadRequest(); }
            var getCustomer = await _customerSvc.GetTheCustomerByCustomerCode(customer_code);
            if(getCustomer == null) { return NotFound(); }
            return Ok(getCustomer);
        }

        /// <summary>
        /// Get the customers by customer name/phone number
        /// </summary>
        /// <param name="str">search string</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid customer list</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTheCustomersBySearchString([FromQuery] string str, [FromQuery] int limit)
        {
            IEnumerable<Customer> getCustomers;
            if(string.IsNullOrEmpty(str)) { return BadRequest(); }

            var byCustomerName = await _customerSvc.GetTheCustomersByCustomerName(str);
            getCustomers = limit > 0 ? byCustomerName.Take(limit) : byCustomerName;

            if(getCustomers.Count() >= limit) { return Ok(getCustomers); }

            var byPhoneNumber = await _customerSvc.GetTheCustomerByPhoneNumber(str);
            getCustomers = limit > 0 ? getCustomers.Union(byPhoneNumber).Take(limit - getCustomers.Count()) : getCustomers.Union(byPhoneNumber);

            return Ok(getCustomers);
        }

        /// <summary>
        /// Get the customers by branch code
        /// </summary>
        /// <param name="branch_code">branch code</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid customer list</returns>
        [HttpGet("{branch_code}/branches")]
        public async Task<IActionResult> GetTheCustomersByBranchCode(string branch_code, [FromQuery] int limit)
        {
            if(string.IsNullOrEmpty(branch_code)) { return BadRequest(); }
            IEnumerable<Customer> getCustomers = await _customerSvc.GetTheCustomersByBranchCode(branch_code);
            if(limit <= 0) { return Ok(getCustomers); }
            return Ok(getCustomers.Take(limit));
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="customer">customer data</param>
        /// <returns>customer has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewCustomer([FromBody] Customer customer)
        {
            if(customer == null) { return BadRequest(); }
            var newCustomer = await _customerSvc.AddANewCustomer(customer);
            return Ok(newCustomer);
        }

        /// <summary>
        /// Edit an exist customer
        /// </summary>
        /// <param name="customer">customer data</param>
        /// <returns>customer has edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistCustomer([FromBody] Customer customer)
        {
            if(customer == null) { return BadRequest(); }
            var editCustomer = await _customerSvc.EditAnExistCustomer(customer);
            if(editCustomer == null) { return NotFound(); }
            return Ok(editCustomer);
        }

        /// <summary>
        /// Delete an exist customer
        /// </summary>
        /// <param name="customer_code">customer code</param>
        /// <returns>customer has deleted</returns>
        [HttpDelete("{customer_code}")]
        public async Task<IActionResult> DeleteAnExistCustomer(string customer_code)
        {
            if(string.IsNullOrEmpty(customer_code)) { return BadRequest(); }
            var delCustomer = await _customerSvc.DeleteAnExistCustomer(customer_code);
            if(delCustomer == null) { return NotFound(); }
            return Ok(delCustomer);
        }
    }
}
