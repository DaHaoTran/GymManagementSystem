using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WorkingCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }

        public DateTime CheckDate { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string CheckOf { get; set; }

        public bool IsCheckIn { get; set; }

        //[ForeignKey("CheckOf")]
        //public Account? account { get; set; }
    }
}
