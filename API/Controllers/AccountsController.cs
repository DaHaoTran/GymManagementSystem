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
       /// <param name="limit">data retrieval limit</param>
       /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Account>> GetAccountList([FromQuery] int limit)
        {
            return await _accountSvc.GetAccountList(limit);
        }

        /// <summary>
        /// Get the account by account code
        /// </summary>
        /// <param name="account_code">account code</param>
        /// <returns>a valid account</returns>
        [HttpGet("{account_code}")]
        public async Task<IActionResult> GetTheAccountByAccountCode(string account_code)
        {
            if(string.IsNullOrEmpty(account_code)) { return BadRequest(); }
            var getAccount = await _accountSvc.GetTheAccountByAccountCode(account_code);
            if(getAccount == null) { return NotFound(); }
            return Ok(getAccount);
        }

        /// <summary>
        /// Get the accounts by full name/address/phone number/id number
        /// </summary>
        /// <param name="str">search string</param>
        /// <param name="limit">data retrieval limit</param>
        /// <returns>valid account list</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetTheAccountsBySearchString([FromQuery] string str, [FromQuery] int limit)
        {
            IEnumerable<Account> getAccounts;
            if(string.IsNullOrEmpty(str)) { return BadRequest(); }

            var byFullName = await _accountSvc.GetTheAccountsByFullName(str);
            getAccounts = limit > 0 ? byFullName.Take(limit) : byFullName;
            if(getAccounts.Count() >= limit) { return Ok(getAccounts); }

            var byAddress = await _accountSvc.GetTheAccountsByAddress(str);
            getAccounts = limit > 0 ? getAccounts.Union(byAddress).Take(limit - getAccounts.Count()) : getAccounts.Union(byAddress);
            if(getAccounts.Count() >= limit) { return Ok(getAccounts); }

            var byPhoneNumber = await _accountSvc.GetTheAccountsByPhoneNumber(str);
            getAccounts = limit > 0 ? getAccounts.Union(byPhoneNumber).Take(limit - getAccounts.Count()) : getAccounts.Union(byPhoneNumber);
            if (getAccounts.Count() >= limit) { return Ok(getAccounts); }

            var byIdNumber = await _accountSvc.GetTheAccountsByIdNumber(str);
            getAccounts = limit > 0 ? getAccounts.Union(byIdNumber).Take(limit - getAccounts.Count()) : getAccounts.Union(byIdNumber);

            return Ok(getAccounts);
        }

        /// <summary>
        /// Add a new account
        /// </summary>
        /// <param name="account">account data</param>
        /// <returns>account has added</returns>
        [HttpPost]
        public async Task<IActionResult> AddANewAccount([FromBody] Account account)
        {
            if(account == null) { return BadRequest(); }
            var newAccount = await _accountSvc.AddANewAccount(account);
            
            var getAccount = await _accountSvc.GetTheAccountsByIdNumber(account.IdNumber);
            if(getAccount.Count() <= 0) { getAccount = await _accountSvc.GetTheAccountsByPhoneNumber(account.PhoneNumber); }
            if (getAccount.Count() <= 0) { return NotFound(); }

            return Ok(getAccount.First());
        }

        /// <summary>
        /// Edit an exist account
        /// </summary>
        /// <param name="account">account data</param>
        /// <returns>account has edited</returns>
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
        /// <returns>account has deleted</returns>
        [HttpDelete("{account_code}")]
        public async Task<IActionResult> DeleteAnExistAccount(string account_code)
        {
            if(string.IsNullOrEmpty(account_code)) { return BadRequest(); }
            var delAccount = await _accountSvc.DeleteAnExistAccount(account_code);
            if(delAccount == null) { return NotFound(); }
            return Ok(delAccount);
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateAccount(Login login)
        {
            if(login == null) { return BadRequest(); };
            var getAccount = await _accountSvc.ValidateAccount(login);
            if(getAccount == null) { return NotFound(); }
            return Ok(getAccount);
        }
    }
}
