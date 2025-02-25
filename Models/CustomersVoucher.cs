using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CustomersVoucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }

        [Column(TypeName = "Varchar(12)")]
        public string CustomerCode { get; set; }

        [Column(TypeName = "Varchar(5)")]
        public string PackageCode { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string UpdateBy { get; set; }

        //[ForeignKey("CustomerCode")]
        //public Customer? customer { get; set; }

        //[ForeignKey("PackageCode")]
        //public ServicePackage? servicePackage { get; set; }
    }
}
