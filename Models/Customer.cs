﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(CustomerCode), nameof(PhoneNumber))]
    public class Customer
    {
        [Key]
        [Column(TypeName = "Varchar(12)", Order = 0)]
        public string CustomerCode { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number does not include letters, special characters")]
        [StringLength(10, ErrorMessage = "Phone number must be 10 digits", MinimumLength = 10)]
        [Key, Column(TypeName = "Char(10)", Order = 1)]
        public string PhoneNumber { get; set; }

        public bool IsBanned { get; set; }

        public string? BannedReason { get; set; }

        [Column(TypeName = "Varchar(6)")]
        public string BranchCode { get; set; }

        [Column(TypeName = "Varchar(10)")]
        public string UpdateBy { get; set; }

        //[ForeignKey("BranchCode")]
        //public Branch? branch { get; set; }

        //public ICollection<CustomersVoucher>? customersVouchers { get; set; }

        //public ICollection<Fine>? fines { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Branch name is required")]
        public string BranchName { get; set; }
    }
}
