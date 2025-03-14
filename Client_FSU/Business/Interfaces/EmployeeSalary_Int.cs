using Models;

namespace Client_FSU.Business.Interfaces
{
    public interface EmployeeSalary_Int
    {
        Task<List<EmployeeSalary>> GetEmployeeSalaryList(string sort, int limit);
        Task<EmployeeSalary> GetTheEmployeeSalaryByEmployeeSalaryCode(Guid empSalCode);
        Task<List<EmployeeSalary>> GetTheEmployeeSalariesBySearchString(string str, int limit);
        Task<List<EmployeeSalary>> GetTheEmployeeSalariesByAccountCode(string accountCode, string sort, int limit);
        Task<EmployeeSalary> AddANewEmployeeSalary(EmployeeSalary employeeSalary);
        Task<EmployeeSalary> EditAnExistEmployeeSalary(EmployeeSalary employeeSalary);
        Task<EmployeeSalary> DeleteAnExistEmployeeSalary(Guid empSalCode);
        Task<List<EmployeeSalary>> GetTheEmployeeSalariesByMonth(int month, int year);
    }
}
