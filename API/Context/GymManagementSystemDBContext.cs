using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBA.Context
{
    public class GymManagementSystemDBContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomersVoucher> CustomersVouchers { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<ServicePackage> ServicePackages { get; set; }
        public DbSet<WorkingCheck> WorkingChecks { get; set; }
    }
}
