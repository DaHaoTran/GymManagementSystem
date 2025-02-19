using Models;

namespace API.Services.Interfaces
{
    public interface WorkingCheck_Int
    {
        List<WorkingCheck> GetWorkingCheckList();
        WorkingCheck GetTheWorkingCheckByOrderNumber(int orderNumber);
        WorkingCheck AddANewWorkingCheck(WorkingCheck workingCheck);
        WorkingCheck EditAnExistWorkingCheck(WorkingCheck workingCheck);
        WorkingCheck DeleteAnExistWorkingCheck(int orderNumber);
        List<WorkingCheck> GetTheWorkingChecksByAccountCode(string accountCode);
    }
}
