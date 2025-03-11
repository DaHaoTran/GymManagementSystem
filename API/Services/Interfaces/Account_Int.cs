using Models;

namespace API.Services.Interfaces
{
    public interface Account_Int
    {
        Task<List<Account>> GetAccountList(int limit);
        Task<Account> GetTheAccountByAccountCode(string accountCode);
        Task<List<Account>> GetTheAccountsByFullName(string fullName);
        Task<List<Account>> GetTheAccountsByPhoneNumber(string phoneNumber);
        Task<List<Account>> GetTheAccountsByIdNumber(string idNumber);
        Task<List<Account>> GetTheAccountsByAddress(string address);
        Task<Account> AddANewAccount(Account account);
        Task<Account> EditAnExistAccount(Account account);
        Task<Account> DeleteAnExistAccount(string accountCode);
        Task<Account> ValidateAccount(Login login);
    }
}
