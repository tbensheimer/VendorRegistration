using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Company
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public DateTime Sign_Up { get; set; }
        [Required]
        public string? Fed_Id_SSN { get; set; }
        [Required]
        public bool EmployeeIdNumber { get; set; }
        [Required]
        public string Supplier_Category { get; set; } = default!;
        public string? Website { get; set; } = default!;
        public bool Minority { get; set; }
        [Required]
        public string Diversity_Certification { get; set; } = default!;
        public string? Other_Diversity_Certification { get; set; } = default!;
        [Required]
        public string Description_Goods_Services { get; set; } = default!;
        public bool Disabled_From_Notifications { get; set; }
        public bool Is_Deleted { get; set; }
        public bool Completed_Registeration { get; set; }
        public DateTime EditedSince { get; set; }
    }
}
