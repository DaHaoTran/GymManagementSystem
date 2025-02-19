using Models;

namespace API.Services.Interfaces
{
    public interface Branch_Int
    {
        List<Branch> GetBranchList();
        Branch GetTheBranchByBranchCode(string branchCode);
        List<Branch> GetTheBranchesByBranchName(string branchName);
        List<Branch> GetTheBranchesByAddress(string address);
        Branch AddANewBranch(Branch branch);
        Branch EditAnExistBranch(Branch branch);
        Branch DeleteAnExistBranch(string branchCode);
    }
}
