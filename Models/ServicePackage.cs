using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ServicePackage
    {
        [Key]
        [Column(TypeName = "Varchar(5)")]
        public string PackageCode { get; set; }

        [Required(ErrorMessage = "Package name is required")]
        public string PackageName { get; set; }

        [Column(TypeName = "Money")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Member quantity is required")]
        [Range(1, 100, ErrorMessage = "Member quantity is greater than 1", MaximumIsExclusive = false)]
        public int MemberQuantity { get; set; }

        [Required(ErrorMessage = "Number of days is required")]
        [Range(1, 999, ErrorMessage = "Member quantity is greater than 1", MaximumIsExclusive = false)]
        public int NumberOfDays { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string AdminUpdate { get; set; }

        //public ICollection<CustomersVoucher>? customersVoucher { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Prices apply is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Prices apply is number")]
        public string GetPrice { get; set; }
    }
}
