using Models;

namespace API.Services.Interfaces
{
    public interface EmployeeSalary_Int
    {
        Task<List<EmployeeSalary>> GetEmployeeSalaryList(int limit);
        Task<EmployeeSalary> GetTheEmployeeSalaryByEmployeeSalaryCode(Guid employeeSalaryCode);
        Task<List<EmployeeSalary>> GetTheEmployeeSalariesByFullName(string fullName);
        Task<List<EmployeeSalary>> GetTheEmployeeSalariesByBranchName(string branchName);
        Task<List<EmployeeSalary>> GetTheEmployeeSalariesByAccountCode(string accountCode);
        Task<EmployeeSalary> AddANewEmployeeSalary(EmployeeSalary employeeSalary);
        Task<EmployeeSalary> EditAnExistEmployeeSalary(EmployeeSalary employeeSalary);
        Task<EmployeeSalary> DeleteAnExistEmployeeSalary(Guid employeeSalaryCode);
    }
}
