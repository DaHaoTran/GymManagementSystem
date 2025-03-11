using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Services.Implements
{
    public class Account_Imp : Account_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public Account_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        
        public async Task<Account> AddANewAccount(Account account)
        {
            await _dBContext.Accounts.AddAsync(account);
            await _dBContext.SaveChangesAsync();
            return account;
        }

        public async Task<Account> DeleteAnExistAccount(string accountCode)
        {
            var getAccount = await GetTheAccountByAccountCode(accountCode);
            if(getAccount == null) { return getAccount; }

            _dBContext.Accounts.Remove(getAccount);
            await _dBContext.SaveChangesAsync();

            return getAccount;
        }

        public async Task<Account> EditAnExistAccount(Account account)
        {
            var getAccount = await GetTheAccountByAccountCode(account.AccountCode);
            if(getAccount == null) { return getAccount; }

            getAccount.FullName = account.FullName;
            getAccount.Age = account.Age;
            getAccount.PhoneNumber = account.PhoneNumber;
            getAccount.IdNumber = account.IdNumber;
            getAccount.LivingAt = account.LivingAt;
            getAccount.Password = account.Password;
            getAccount.UpdateBy = account.UpdateBy;
            getAccount.RoleId = account.RoleId;
            getAccount.IsDeleted = account.IsDeleted;
            getAccount.SalaryCode = account.SalaryCode;

            await _dBContext.SaveChangesAsync();

            return getAccount;
        }

        public Task<List<Account>> GetAccountList(int limit)
        {
            if(limit <= 0) { return _dBContext.Accounts.ToListAsync(); } 
            return _dBContext.Accounts.Take(limit).ToListAsync();
        }

        public async Task<Account> GetTheAccountByAccountCode(string accountCode)
        {
            return await _dBContext.Accounts.Where(x => x.AccountCode == accountCode).FirstOrDefaultAsync();
        }

        public async Task<List<Account>> GetTheAccountsByIdNumber(string idNumber)
        {
            return await _dBContext.Accounts.Where(x => x.IdNumber == idNumber).ToListAsync();
        }

        public async Task<List<Account>> GetTheAccountsByPhoneNumber(string phoneNumber)
        {
            return await _dBContext.Accounts.Where(x => x.PhoneNumber == phoneNumber).ToListAsync();
        }

        public async Task<List<Account>> GetTheAccountsByAddress(string address)
        {
            return await _dBContext.Accounts.Where(x => x.LivingAt.Contains(address)).ToListAsync();
        }

        public async Task<List<Account>> GetTheAccountsByFullName(string fullName)
        {
            return await _dBContext.Accounts.Where(x => x.FullName.Contains(fullName)).ToListAsync();
        }

        public async Task<Account> ValidateAccount(Login login)
        {
            var getAccount = await _dBContext.Accounts.Where(x => x.AccountCode == login.AccountCode).FirstOrDefaultAsync();
            if(getAccount == null || getAccount == default) { return null!; }
            if(getAccount.Password != login.Password) { return null!; }
            return getAccount;
        }
    }
}
