using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class VendorAccount
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        [Required]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Invalid email address. Format: XXXX@example.com")]
        public string? Email { get; set; } = default!;
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){6,1000}$",
         ErrorMessage = "Password doesn't meet security rules. Password must include at least one: Number, Uppercase Letter, Lowercase Letter, and non-alplha-numeric character. Password Length between 6 and 1000 characters (no spaces).")]
        public string? Password { get; set; } = default!;
    }
}
