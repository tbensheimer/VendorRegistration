using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Authorized_Contacts
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string? FirstName { get; set; } = default!;
        [Required]
        public string? LastName { get; set; } = default!;
        [Required]
        [RegularExpression(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$", ErrorMessage = "Invalid email address. Format: XXXX@example.com")]
        public string? Email { get; set; } = default!;
        [Required]
        [RegularExpression(@"^(\d{3}-\d{3}-\d{4}|\d{10}|(\d{3})\s?\d{3}-\d{4})$", ErrorMessage = "Invalid phone number. Format: XXXXXXXXXX or XXXXXXXXXXX or XXX XXX XXXX or X XXX XXX XXXX")]
        public double? Phone_Number { get; set; }
        public bool Contact_Deleted { get; set; }
    }
}
