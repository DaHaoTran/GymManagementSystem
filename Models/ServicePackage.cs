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

        public string PackageName { get; set; }

        public int MemberQuantity { get; set; }

        public int NumberOfDays { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string AdminUpdate { get; set; }
    }
}
