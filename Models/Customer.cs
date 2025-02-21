using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        [Key]
        [Column(TypeName = "Varchar(12)")]
        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        [Column(TypeName = "Char(10)")]
        public string PhoneNumber { get; set; }

        public bool IsBanned { get; set; }

        public string? BannedReason { get; set; }

        [Column(TypeName = "Varchar(6)")]
        public string BranchCode { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string UpdateBy { get; set; }

        [ForeignKey("BranchCode")]
        public Branch? branch;

        public ICollection<CustomersVoucher>? customersVouchers;

        public ICollection<Fine>? fines;
    }
}
