using Models;

namespace API.Services.Interfaces
{
    public interface Salary_Int
    {
        List<Salary> GetSalaryList();
        Salary GetTheSalaryBySalaryCode(string salaryCode);
        Salary AddANewSalary(Salary salary);
        Salary EditAnExistSalary(Salary salary);
        Salary DeleteAnExistSalary(string salaryCode);
    }
}
