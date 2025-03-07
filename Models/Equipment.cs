using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Equipment
    {
        [Key]
        [Column(TypeName = "Varchar(10)")]
        public string EquipCode { get; set; }

        [Required(ErrorMessage = "Branch code is required")]
        [Column(TypeName = "Varchar(6)")]
        public string BranchCode { get; set; }

        [Required(ErrorMessage = "Equip name is required")]
        public string EquipName { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        public string? Note { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string? StaffUpdate { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string AdminUpdate { get; set; }

        public bool IsReceived { get; set; }

        public bool IsDeleted { get; set; }

        //[ForeignKey("BranchCode")]
        //public Branch? branch { get; set; }
    }
}
