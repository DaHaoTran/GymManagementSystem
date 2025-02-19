using Models;

namespace API.Services.Interfaces
{
    public interface Account_Int
    {
        Task<List<Account>> GetAccountList();
        Task<Account> GetTheAccountByAccountCode(string accountCode);
        Task<List<Account>> GetTheAccountsByFullName(string fullName);
        Task<Account> GetTheAccountByPhoneNumber(string phoneNumber);
        Task<Account> GetTheAccountByIdNumber(string idNumber);
        Task<List<Account>> GetTheAccountsByAddress(string address);
        Task<Account> AddANewAccount(Account account);
        Task<Account> EditAnExistAccount(Account account);
        Task<Account> DeleteAnExistAccount(string accountCode);
    }
}
