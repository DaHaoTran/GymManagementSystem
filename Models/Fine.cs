using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Fine
    {
        [Key]
        public Guid FineCode { get; set; }

        [Column(TypeName = "Varchar(12)")]
        public string CustomerCode { get; set; }

        public DateTime Date { get; set; }

        public string Reason { get; set; }

        public bool IsCompensated { get; set; }

        public string? AdminNote { get; set; }

        public string? StaffNote { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string UpdateBy { get; set; }

        [ForeignKey("CustomerCode")]
        public Customer? customer;
    }
}
