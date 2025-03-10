using Models;

namespace Client_FAU.Business.Interfaces
{
    public interface WorkingCheck_Int
    {
        Task<List<WorkingCheck>> GetWorkingCheckList(string sort, int limit);
        Task<WorkingCheck> GetTheWorkingCheckByOrderNumber(int orderNumber);
        Task<List<WorkingCheck>> GetTheWorkingChecksByAccountCode(string accountCode, string sort, int limit);
        Task<WorkingCheck> AddANewWorkingCheck(WorkingCheck workCheck);
        Task<WorkingCheck> EditAnExistWorkingCheck(WorkingCheck workCheck);
        Task<WorkingCheck> DeleteAnExistWorkingCheck(int orderNumber);
    }
}
