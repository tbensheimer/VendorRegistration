using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string? Address_1 { get; set; } = default!;
        public string? Address_2 { get; set; } = default!;
        [Required]
        public string? City { get; set; } = default!;
        [Required]
        public string? State_Region_Providence { get; set; } = default!;
        public string? Postal_Code { get; set; }
        [Required]
        public string? Country { get; set; } = default!;
        public bool International { get; set; }

    }
}
