using Models;

namespace Client_FSU.Business.Interfaces
{
    public interface Branch_Int
    {
        Task<List<Branch>> GetBranchList(int limit);
        Task<Branch> AddNewBranch(Branch branch);
        Task<Branch> EditAnExistBranch(Branch branch);
        Task<Branch> DeleteAnExistBranch(string branchCode);
        Task<Branch> GetTheBranchByBranchCode(string branchCode);
        Task<List<Branch>> GetTheBranchesBySearchString(string str, int limit);
    }
}
