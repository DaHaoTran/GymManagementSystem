using Models;

namespace Client_FAU.Business.Interfaces
{
    public interface Fine_Int
    {
        Task<List<Fine>> GetFineList(string sort, int limit);
        Task<Fine> GetTheFineByFineCode(Guid fineCode);
        Task<List<Fine>> GetTheFinesByCustomerCode(string customerCode, string sort, int limit);
        Task<Fine> AddANewFine(Fine fine);  
        Task<Fine> EditAnExistFine(Fine fine);
        Task<Fine > DeleteAnExistFine(Guid fineCode);
    }
}
