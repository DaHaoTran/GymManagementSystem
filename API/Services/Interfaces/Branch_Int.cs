using Models;

namespace API.Services.Interfaces
{
    public interface Branch_Int
    {
        Task<List<Branch>> GetBranchList();
        Task<Branch> GetTheBranchByBranchCode(string branchCode);
        Task<List<Branch>> GetTheBranchesByBranchName(string branchName);
        Task<List<Branch>> GetTheBranchesByAddress(string address);
        Task<Branch> AddANewBranch(Branch branch);
        Task<Branch> EditAnExistBranch(Branch branch);
        Task<Branch> DeleteAnExistBranch(string branchCode);
    }
}
