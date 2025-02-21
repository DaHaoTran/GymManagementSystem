using API.Services.Interfaces;
using DBA.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks;

namespace API.Services.Implements
{
    public class Salary_Imp : Salary_Int
    {
        private readonly GymManagementSystemDBContext _dBContext;
        public Salary_Imp(GymManagementSystemDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Salary> AddANewSalary(Salary salary)
        {
            await _dBContext.Salaries.AddAsync(salary);
            await _dBContext.SaveChangesAsync();
            return salary;
        }

        public async Task<Salary> DeleteAnExistSalary(string salaryCode)
        {
            var getSalary = await GetTheSalaryBySalaryCode(salaryCode);
            if(getSalary == null) { return getSalary; }

            _dBContext.Salaries.Remove(getSalary);
            await _dBContext.SaveChangesAsync();

            return getSalary;
        }

        public async Task<Salary> EditAnExistSalary(Salary salary)
        {
            var getSalary = await GetTheSalaryBySalaryCode(salary.SalaryCode);
            if(getSalary == null) { return salary; }

            getSalary.SalaryType = salary.SalaryType;
            getSalary.PricesApply = salary.PricesApply;
            getSalary.UpdateDate = salary.UpdateDate;

            await _dBContext.SaveChangesAsync();

            return getSalary;
        }

        public async Task<List<Salary>> GetSalaryList()
        {
            return await _dBContext.Salaries.ToListAsync();
        }

        public async Task<Salary> GetTheSalaryBySalaryCode(string salaryCode)
        {
            return await _dBContext.Salaries.Where(x => x.SalaryCode == salaryCode).FirstOrDefaultAsync();
        }
    }
}
