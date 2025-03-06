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

        public GymManagementSystemDBContext(DbContextOptions<GymManagementSystemDBContext> options): base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.account) 
                .WithMany(a => a.branches) 
                .HasForeignKey(b => b.AdminUpdate) 
                .HasPrincipalKey(a => a.AccountCode); 

            modelBuilder.Entity<EmployeeSalary>()
                .HasOne(e => e.account)
                .WithMany(a => a.employeeSalaries)
                .HasForeignKey(e => e.AccountCode)
                .HasPrincipalKey(a => a.AccountCode);

            modelBuilder.Entity<WorkingCheck>()
                .HasOne(w => w.account)
                .WithMany(a => a.workingChecks)
                .HasForeignKey(w => w.CheckOf)
                .HasPrincipalKey(a => a.AccountCode);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.branch)
                .WithMany(b => b.customers)
                .HasForeignKey(c => c.BranchCode)
                .HasPrincipalKey(b => b.BranchCode);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.branch)
                .WithMany(b => b.equipment)
                .HasForeignKey(e => e.BranchCode)
                .HasPrincipalKey(b => b.BranchCode);

            modelBuilder.Entity<CustomersVoucher>()
                .HasOne(c => c.customer)
                .WithMany(c => c.customersVouchers)
                .HasForeignKey(c => c.CustomerCode)
                .HasPrincipalKey(c => c.CustomerCode);

            modelBuilder.Entity<Fine>()
                .HasOne(f => f.customer)
                .WithMany(c => c.fines)
                .HasForeignKey(f => f.CustomerCode)
                .HasPrincipalKey(c => c.CustomerCode);

            modelBuilder.Entity<Account>()
                .HasOne(s => s.salary)
                .WithMany(a => a.account)
                .HasForeignKey(s => s.SalaryCode)
                .HasPrincipalKey(a => a.SalaryCode);

            modelBuilder.Entity<CustomersVoucher>()
                .HasOne(s => s.servicePackage)
                .WithMany(s => s.customersVoucher)
                .HasForeignKey(s => s.PackageCode)
                .HasPrincipalKey(s => s.PackageCode);
        }

    }
}
