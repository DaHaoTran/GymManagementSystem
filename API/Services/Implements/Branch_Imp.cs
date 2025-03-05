using API.Services.Interfaces;

using DBA.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace API.Services.Implements
{
    public class Branch_Imp : Branch_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public Branch_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        
        public async Task<Branch> AddANewBranch(Branch branch)
        {
            await _dBContext.Branches.AddAsync(branch);
            await _dBContext.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch> DeleteAnExistBranch(string branchCode)
        {
            var getBranch = await GetTheBranchByBranchCode(branchCode);
            if(getBranch == null) { return getBranch; }

            _dBContext.Branches.Remove(getBranch);
            await _dBContext.SaveChangesAsync();

            return getBranch;
        }

        public async Task<Branch> EditAnExistBranch(Branch branch)
        {
            var getBranch = await GetTheBranchByBranchCode(branch.BranchCode);
            if(getBranch == null) { return getBranch; }

            getBranch.BranchName = branch.BranchName;
            getBranch.Address = branch.Address;
            getBranch.QuantityOfPTs = branch.QuantityOfPTs;
            getBranch.QuantityOfStaffs = branch.QuantityOfStaffs;
            getBranch.QuantityOfWorkers = branch.QuantityOfWorkers;
            getBranch.AdminUpdate = branch.AdminUpdate;
            getBranch.IsDeleted = branch.IsDeleted;

            await _dBContext.SaveChangesAsync();

            return getBranch;
        }

        public async Task<List<Branch>> GetBranchList(int limit)
        {
            if(limit <= 0) { return await _dBContext.Branches.ToListAsync(); }
            return await _dBContext.Branches.Take(limit).ToListAsync();
        }

        public async Task<Branch> GetTheBranchByBranchCode(string branchCode)
        {
            return await _dBContext.Branches.Where(x => x.BranchCode == branchCode).FirstOrDefaultAsync();
        }

        public async Task<List<Branch>> GetTheBranchesByAddress(string address)
        {
            return await _dBContext.Branches.Where(x => x.Address.Contains(address, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<List<Branch>> GetTheBranchesByBranchName(string branchName)
        {
            return await _dBContext.Branches.Where(x => x.BranchName.Contains(branchName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
    }
}
