using Models;
using System.ComponentModel.DataAnnotations;

namespace PasswordManipulate.Data
{
    public class LoginCP: Login
    {
        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; }
    }
}
