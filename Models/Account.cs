using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account
    {
        [Key]
        [Column(TypeName = "Varchar(10)")]
        public string AccountCode { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        [Column(TypeName = "Char(10)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "Char(12)")]
        public string IdNumber { get; set; }

        public string LivingAt { get; set; }

        public string Password { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string UpdateBy { get; set; }

        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "Varchar(5)")]
        public string SalaryCode { get; set; }

        public ICollection<Branch>? branches;

        public ICollection<WorkingCheck>? workingChecks;

        [ForeignKey("RoleId")]
        public Role? role;

        [ForeignKey("SalaryCode")]
        public Salary? salary;

        public ICollection<EmployeeSalary>? employeeSalaries;
    }
}
