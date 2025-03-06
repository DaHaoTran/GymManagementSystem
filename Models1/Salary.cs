using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(SalaryCode), nameof(SalaryType))]
    public class Salary
    {
        [Key]
        [Column(TypeName = "Varchar(5)", Order = 0)]
        public string SalaryCode { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Salary type is required")]
        public string SalaryType { get; set; }

        [Column(TypeName = "Money")]
        public double PricesApply { get; set; }

        public DateTime UpdateDate { get; set; }

        //public IEnumerable<Account>? account { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Prices apply is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Prices apply is number")]
        public string GetPricesApply { get; set; }
    }
}
