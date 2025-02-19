using Models;

namespace API.Services.Interfaces
{
    public interface WorkingCheck_Int
    {
        Task<List<WorkingCheck>> GetWorkingCheckList();
        Task<WorkingCheck> GetTheWorkingCheckByOrderNumber(int orderNumber);
        Task<WorkingCheck> AddANewWorkingCheck(WorkingCheck workingCheck);
        Task<WorkingCheck> EditAnExistWorkingCheck(WorkingCheck workingCheck);
        Task<WorkingCheck> DeleteAnExistWorkingCheck(int orderNumber);
        Task<List<WorkingCheck>> GetTheWorkingChecksByAccountCode(string accountCode);
    }
}
