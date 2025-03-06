using Models;

namespace API.Services.Interfaces
{
    public interface Salary_Int
    {
        Task<List<Salary>> GetSalaryList(int limit);
        Task<Salary> GetTheSalaryBySalaryCode(string salaryCode);
        Task<List<Salary>> GetTheSalaryBySalaryType(string salaryType);
        Task<Salary> AddANewSalary(Salary salary);
        Task<Salary> EditAnExistSalary(Salary salary);
        Task<Salary> DeleteAnExistSalary(string salaryCode);
    }
}
