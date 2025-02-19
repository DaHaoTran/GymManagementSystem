using Models;

namespace API.Services.Interfaces
{
    public interface Account_Int
    {
        List<Account> GetAccountList();
        Account GetTheAccountByAccountCode(string accountCode);
        List<Account> GetTheAccountsByFullName(string fullName);
        Account GetTheAccountByPhoneNumber(string phoneNumber);
        Account GetTheAccountByIdNumber(string idNumber);
        List<Account> GetTheAccountsByAddress(string address);
        Account AddANewAccount(Account account);
        Account EditAnExistAccount(Account account);
        Account DeleteAnExistAccount(string accountCode);
    }
}
