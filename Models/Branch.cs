using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Branch
    {
        [Key]
        [Column(TypeName = "Varchar(6)")]
        public string BranchCode { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }

        public int QuantityOfStaffs { get; set; }
    }
}
