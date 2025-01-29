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

        public int QuantityOfPTs { get; set; }

        public int QuantityOfWorkers { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string AdminUpdate { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("AdminUpdate")]
        public Account? account;

        public ICollection<Equipment>? equipment;

        public ICollection<Customer>? customers;
    }
}
