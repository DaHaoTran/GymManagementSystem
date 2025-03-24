using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client_FSU.Models
{
    public class CusCustomer
    {
        [DisplayName("Customer name")]
        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number does not include letters, special characters")]
        [StringLength(10, ErrorMessage = "Phone number must be 10 digits", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [DisplayName("Branch name")]
        [Required(ErrorMessage = "Branch name is required")]
        public string BranchName { get; set; }
    }
}
