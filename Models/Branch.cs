using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [Column(TypeName = "Varchar(6)", Order = 0)]
        public string BranchCode { get; set; }

        [Required(ErrorMessage = "Branch name is required")]
        public string BranchName { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public int QuantityOfStaffs { get; set; }

        public int QuantityOfPTs { get; set; }

        public int QuantityOfWorkers { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string AdminUpdate { get; set; }

        public bool IsDeleted { get; set; }

        //[ForeignKey("AdminUpdate")]
        //public Account? account { get; set; }

        //public ICollection<Equipment>? equipment { get; set; }

        //public ICollection<Customer>? customers { get; set; }
    }
}
