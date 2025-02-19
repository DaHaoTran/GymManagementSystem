using Models;

namespace API.Services.Interfaces
{
    public interface EmployeeSalary_Int
    {
        List<EmployeeSalary> GetEmployeeSalaryList();
        EmployeeSalary GetTheEmployeeSalaryByEmployeeSalaryCode(Guid employeeSalaryCode);
        List<EmployeeSalary> GetTheEmployeeSalariesByFullName(string fullName);
        List<EmployeeSalary> GetTheEmployeeSalariesByBranchName(string branchName);
        List<EmployeeSalary> GetTheEmployeeSalariesByAccountCode(string accountCode);
        EmployeeSalary AddANewEmployeeSalary(EmployeeSalary employeeSalary);
        EmployeeSalary EditAnExistEmployeeSalary(EmployeeSalary employeeSalary);
        EmployeeSalary DeleteAnExistEmployeeSalary(Guid employeeSalaryCode);
    }
}
