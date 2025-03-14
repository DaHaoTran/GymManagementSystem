using Models;

namespace Client_FSU.Business.Interfaces
{
    public interface Salary_Int
    {
        Task<List<Salary>> GetSalaryList(int limit);
        Task<Salary> GetTheSalaryBySalaryCode(string salaryCode);
        Task<Salary> AddANewSalary(Salary salary);
        Task<Salary> EditAnExistSalary(Salary salary);
        Task<Salary> DeleteAnExistSalary(string salaryCode);
    }
}
