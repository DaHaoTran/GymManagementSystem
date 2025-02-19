using Models;

namespace API.Services.Interfaces
{
    public interface Fine_Int
    {
        Task<List<Fine>> GetFineList();
        Task<Fine> GetTheFineByFineCode(Guid fineCode);
        Task<List<Fine>> GetTheFinesByCustomerCode(string customerCode);
        Task<Fine> AddANewFine(Fine fine);
        Task<Fine> EditAnExistFine(Fine fine);
        Task<Fine> DeleteAnExistFine(Guid fineCode);
    }
}
