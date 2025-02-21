using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Services.Implements
{
    public class EmployeeSalary_Imp : EmployeeSalary_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public EmployeeSalary_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<EmployeeSalary> AddANewEmployeeSalary(EmployeeSalary employeeSalary)
        {
            await _dBContext.EmployeeSalaries.AddAsync(employeeSalary);
            await _dBContext.SaveChangesAsync();
            return employeeSalary;
        }

        public async Task<EmployeeSalary> DeleteAnExistEmployeeSalary(Guid employeeSalaryCode)
        {
            var getES = await GetTheEmployeeSalaryByEmployeeSalaryCode(employeeSalaryCode);
            if(getES == null) { return getES; }

            _dBContext.EmployeeSalaries.Remove(getES);
            await _dBContext.SaveChangesAsync();

            return getES;
        }

        public async Task<EmployeeSalary> EditAnExistEmployeeSalary(EmployeeSalary employeeSalary)
        {
            var getES = await GetTheEmployeeSalaryByEmployeeSalaryCode(employeeSalary.EmpSalCode);
            if(getES == null) { return getES; }

            getES.FullName = employeeSalary.FullName;
            getES.BranchName = employeeSalary.BranchName;
            getES.Month = employeeSalary.Month;
            getES.WorkDays = employeeSalary.WorkDays;
            getES.PriceTotals = employeeSalary.PriceTotals;
            getES.Note = employeeSalary.Note;
            getES.IsPaid = employeeSalary.IsPaid;
            getES.AccountCode = employeeSalary.AccountCode;
            getES.ProofImage = employeeSalary.ProofImage;

            await _dBContext.SaveChangesAsync();

            return getES;
        }

        public async Task<List<EmployeeSalary>> GetEmployeeSalaryList()
        {
            return await _dBContext.EmployeeSalaries.ToListAsync();
        }

        public async Task<List<EmployeeSalary>> GetTheEmployeeSalariesByAccountCode(string accountCode)
        {
            return await _dBContext.EmployeeSalaries.Where(x => x.AccountCode == accountCode).ToListAsync();
        }

        public async Task<List<EmployeeSalary>> GetTheEmployeeSalariesByBranchName(string branchName)
        {
            return await _dBContext.EmployeeSalaries.Where(x => x.BranchName.Contains(branchName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<List<EmployeeSalary>> GetTheEmployeeSalariesByFullName(string fullName)
        {
            return await _dBContext.EmployeeSalaries.Where(x => x.FullName.Contains(fullName, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }

        public async Task<EmployeeSalary> GetTheEmployeeSalaryByEmployeeSalaryCode(Guid employeeSalaryCode)
        {
            return await _dBContext.EmployeeSalaries.Where(x => x.EmpSalCode == employeeSalaryCode).FirstOrDefaultAsync();
        }
    }
}
