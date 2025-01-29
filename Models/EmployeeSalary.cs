using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EmployeeSalary
    {
        [Key]
        public Guid EmpSalCode { get; set; }

        public string FullName { get; set; }

        public string BranchName { get; set; }

        public DateOnly Month { get; set; }

        public int WorkDays { get; set; }

        [Column(TypeName = "Money")]
        public double PriceTotals { get; set; }

        public string? Note { get; set; }

        public bool IsPaid { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string AccountCode { get; set; }

        [ForeignKey("AccountCode")]
        public Account? account;
    }
}
