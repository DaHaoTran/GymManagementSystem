using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Services.Implements
{
    public class WorkingCheck_Imp : WorkingCheck_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public WorkingCheck_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<WorkingCheck> AddANewWorkingCheck(WorkingCheck workingCheck)
        {
            await _dBContext.WorkingChecks.AddAsync(workingCheck);
            await _dBContext.SaveChangesAsync();
            return workingCheck;
        }

        public async Task<WorkingCheck> DeleteAnExistWorkingCheck(int orderNumber)
        {
            var getWK = await GetTheWorkingCheckByOrderNumber(orderNumber);
            if(getWK == null) { return getWK; }

            _dBContext.WorkingChecks.Remove(getWK);
            await _dBContext.SaveChangesAsync();

            return getWK;
        }

        public async Task<WorkingCheck> EditAnExistWorkingCheck(WorkingCheck workingCheck)
        {
            var getWK = await GetTheWorkingCheckByOrderNumber(workingCheck.OrderNumber);
            if(getWK == null) { return getWK; }

            getWK.CheckDate = workingCheck.CheckDate;
            getWK.CheckOf = workingCheck.CheckOf;
            getWK.IsCheckIn = workingCheck.IsCheckIn;

            await _dBContext.SaveChangesAsync();

            return getWK;
        }

        public async Task<WorkingCheck> GetTheWorkingCheckByOrderNumber(int orderNumber)
        {
            return await _dBContext.WorkingChecks.Where(x => x.OrderNumber == orderNumber).FirstOrDefaultAsync();
        }

        public async Task<List<WorkingCheck>> GetTheWorkingChecksByAccountCode(string accountCode)
        {
            return await _dBContext.WorkingChecks.Where(x => x.CheckOf == accountCode).ToListAsync();
        }

        public async Task<List<WorkingCheck>> GetWorkingCheckList()
        {
            return await _dBContext.WorkingChecks.ToListAsync();
        }
    }
}
