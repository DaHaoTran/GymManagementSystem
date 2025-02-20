using API.Services.Interfaces;
using DBA.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly Account_Int _accountSvc;
        public AccountsController(Account_Int accountSvc)
        {
            _accountSvc = accountSvc;
        }

        /// <summary>
        /// Get account list
        /// </summary>
        /// <returns>account list</returns>
        [HttpGet]
        public async Task<List<Account>> GetAccountList()
        {
            return await _accountSvc.GetAccountList();
        }

        /// <summary>
        /// Get the account by account code
        /// </summary>
        /// <param name="account_code">account code</param>
        /// <returns>a valid account</returns>
        [HttpGet("account-code/{account_code}")]
        public async Task<IActionResult> GetTheAccountByAccountCode(string account_code)
        {
            if(string.IsNullOrEmpty(account_code)) { return BadRequest(); }
            var getAccount = await _accountSvc.GetTheAccountByAccountCode(account_code);
            if(getAccount == null) { return NotFound(); }
            return Ok(getAccount);
        }

        /// <summary>
        /// Get the accounts by full name
        /// </summary>
        /// <param name="full_name">full name</param>
        /// <returns>valid account list</returns>
        [HttpGet("full-name/{full_name}")]
        public async Task<IActionResult> GetTheAccountsByFullName(string full_name)
        {
            if(string.IsNullOrEmpty(full_name)) { return BadRequest(); }
            var getAccounts = await _accountSvc.GetTheAccountsByFullName(full_name);
            return Ok(getAccounts);
        }

        /// <summary>
        /// Get the account by phone number
        /// </summary>
        /// <param name="phone_number">phone number</param>
        /// <returns>a valid account</returns>
        [HttpGet("phone-number/{phone_number}")]
        public async Task<IActionResult> GetTheAccountByPhoneNumber(int phone_number)
        {
            if(phone_number <= 999999999) { return BadRequest(); }
            var getAccount = await _accountSvc.GetTheAccountByPhoneNumber(phone_number);
            if(getAccount == null) { return NotFound(); }
            return Ok(getAccount);
        }

        /// <summary>
        /// Get the account by id number
        /// </summary>
        /// <param name="id_number">id number</param>
        /// <returns>a valid account</returns>
        [HttpGet("id-number/{id_number}")]
        public async Task<IActionResult> GetTheAccountByIdNumber(double id_number)
        {
            if (id_number <= 99999999999) { return BadRequest(); }
            var getAccount = await _accountSvc.GetTheAccountByIdNumber(id_number);
            if(getAccount == null) { return NotFound(); }
            return Ok(getAccount);
        }

        /// <summary>
        /// Get the account by address
        /// </summary>
        /// <param name="address">address</param>
        /// <returns>valid account list</returns>
        [HttpGet("address/{address}")]
        public async Task<IActionResult> GetTheAccountsByAddress(string address)
        {
            if(string.IsNullOrEmpty(address)) { return BadRequest(); }
            var getAccounts = await _accountSvc.GetTheAccountsByAddress(address);
            return Ok(getAccounts);
        }

        /// <summary>
        /// Add a new account
        /// </summary>
        /// <param name="account">account data</param>
        /// <returns>account have added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewAccount([FromBody] Account account)
        {
            if(account == null) { return BadRequest(); }
            var newAccount = await _accountSvc.AddANewAccount(account);
            return Ok(newAccount);
        }

        /// <summary>
        /// Edit an exist account
        /// </summary>
        /// <param name="account">account data</param>
        /// <returns>account have edited</returns>
        [HttpPut]
        public async Task<IActionResult> EditAnExistAccount([FromBody] Account account)
        {
            if(account == null) { return BadRequest(); }
            var editAccount = await _accountSvc.EditAnExistAccount(account);
            if(editAccount == null) { return NotFound(); }
            return Ok(editAccount);
        }

        /// <summary>
        /// Delete an exist account
        /// </summary>
        /// <param name="account_code">account code</param>
        /// <returns>account have deleted</returns>
        [HttpDelete("{account_code}")]
        public async Task<IActionResult> DeleteAnExistAccount(string account_code)
        {
            if(string.IsNullOrEmpty(account_code)) { return BadRequest(); }
            var delAccount = await _accountSvc.DeleteAnExistAccount(account_code);
            if(delAccount == null) { return NotFound(); }
            return Ok(delAccount);
        }
    }
}
