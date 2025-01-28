using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Salary
    {
        [Key]
        [Column(TypeName = "Varchar(5)")]
        public string SalaryCode { get; set; }

        public string SalaryType { get; set; }

        [Column(TypeName = "Money")]
        public double PricesApply { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
