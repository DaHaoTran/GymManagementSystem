using Models;

namespace Client_FSU.Business.Interfaces
{
    public interface Account_Int
    {
        Task<List<Account>> GetAccountList(int limit);
        Task<Account> GetTheAccountByAccountCode(string accountCode);
        Task<List<Account>> GetTheAccountsBySearchString(string str, int limit);
        Task<Account> AddANewAccount(Account account);
        Task<Account> EditAnExistAccount(Account account);
        Task<Account> DeleteAnExistAccount(string accountCode);
        Task<Account> ValidateAccount(Login login);
    }
}
