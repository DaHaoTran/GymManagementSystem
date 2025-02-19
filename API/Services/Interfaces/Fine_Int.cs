using Models;

namespace API.Services.Interfaces
{
    public interface Fine_Int
    {
        List<Fine> GetFineList();
        Fine GetTheFineByFineCode(Guid fineCode);
        List<Fine> GetTheFinesByCustomerCode(string customerCode);
        Fine AddANewFine(Fine fine);
        Fine EditAnExistFine(Fine fine);
        Fine DeleteAnExistFine(Guid fineCode);
    }
}
