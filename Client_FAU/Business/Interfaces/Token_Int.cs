using Models;

namespace Client_FAU.Business.Interfaces
{
    public interface Token_Int
    {
        Task<string> GenerateJwtToken(Account account);
        Task<string> SolveToken(string token);  
    }
}
