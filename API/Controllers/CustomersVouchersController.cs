using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/customers-vouchers")]
    [ApiController]
    public class CustomersVouchersController : ControllerBase
    {
        private readonly CustomersVoucher_Int _cVSvc;
        public CustomersVouchersController(CustomersVoucher_Int cVSvc)
        {
            _cVSvc = cVSvc;
        }

        /// <summary>
        /// Get customers voucher list
        /// </summary>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>customers voucher list</returns>
        [HttpGet]
        public async Task<IEnumerable<CustomersVoucher>> GetCustomersVoucherList([FromQuery] int limit)
        {
            var cVs = await _cVSvc.GetCustomersVoucherList();
            if(limit <= 0) { return cVs; }
            return cVs.Take(limit);
        }

        /// <summary>
        /// Get the customers voucher by order number
        /// </summary>
        /// <param name="order_number">order number</param>
        /// <returns>a valid customers voucher</returns>
        [HttpGet("{order_number}")]
        public async Task<IActionResult> GetTheCustomersVoucherByOrderNumber(int order_number)
        {
            if(order_number <= 0) { return BadRequest(); }
            var getCV = await _cVSvc.GetTheCustomersVoucherByOrderNumber(order_number);
            if(getCV == null) { return NotFound(); }
            return Ok(getCV);
        }

        /// <summary>
        /// Get the customers vouchers by customer code
        /// </summary>
        /// <param name="customer_code">customer code</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid customers voucher list</returns>
        [HttpGet("{customer_code}/customers")]
        public async Task<IActionResult> GetTheCustomersVouchersByCustomerCode(string customer_code, [FromQuery] int limit)
        {
            if(string.IsNullOrEmpty(customer_code)) { return BadRequest(); }
            var getCVs = await _cVSvc.GetTheCustomersVouchersByCustomerCode(customer_code);
            if(limit <= 0) { return Ok(getCVs); }
            return Ok(getCVs.Take(limit));
        }

        /// <summary>
        /// Add a new customers voucher
        /// </summary>
        /// <param name="customersVoucher">customers voucher data</param>
        /// <returns>customers voucher have added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewCustomersVoucher([FromBody] CustomersVoucher customersVoucher)
        {
            if(customersVoucher == null) { return BadRequest(); }
            var newCV = await _cVSvc.AddANewCustomersVoucher(customersVoucher);
            return Ok(newCV);
        }

        /// <summary>
        /// Edit an exist customers voucher
        /// </summary>
        /// <param name="customersVoucher">customers voucher data</param>
        /// <returns>customers voucher have edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistCustomersVoucher([FromBody] CustomersVoucher customersVoucher)
        {
            if(customersVoucher == null) { return BadRequest(); }
            var editCV = await _cVSvc.EditAnExistCustomersVoucher(customersVoucher);
            if(editCV == null) { return NotFound(); }
            return Ok(editCV);
        }

        /// <summary>
        /// Delete an exist customers voucher
        /// </summary>
        /// <param name="order_number">order number</param>
        /// <returns>customers voucher have deleted</returns>
        [HttpDelete("{order_number}")]
        public async Task<IActionResult> DeleteAnExistCustomersVoucher(int order_number)
        {
            if(order_number <= 0) { return BadRequest(); }
            var delCV = await _cVSvc.DeleteAnExistCustomersVoucher(order_number);
            if(delCV == null) { return NotFound(); }
            return Ok(delCV);
        }
    }
}
