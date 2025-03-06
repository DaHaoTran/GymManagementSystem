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
        [Column(TypeName = "Varchar(10)", Order = 0)]
        public string AccountCode { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number does not include letters, special characters")]
        [StringLength(10, ErrorMessage = "Phone number must be 10 digits", MinimumLength = 10)]
        [Key, Column(TypeName = "Char(10)", Order = 1)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Id number is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Id number does not include letters, special characters")]
        [StringLength(12, ErrorMessage = "Id number must be 12 digits", MinimumLength = 12)]
        [Key, Column(TypeName = "Char(12)", Order = 2)]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Living at is required")]
        public string LivingAt { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string UpdateBy { get; set; }

        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "Varchar(5)")]
        public string SalaryCode { get; set; }

        //public ICollection<Branch>? branches { get; set; }

        //public ICollection<WorkingCheck>? workingChecks { get; set; }

        //[ForeignKey("RoleId")]
        //public Role? role { get; set; }

        //[ForeignKey("SalaryCode")]
        //public Salary? salary { get; set; }

        //public ICollection<EmployeeSalary>? employeeSalaries { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Role is required")]
        public string GetRoleName { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Salary type is required")]
        public string GetSalaryType { get; set; }
    }
}
