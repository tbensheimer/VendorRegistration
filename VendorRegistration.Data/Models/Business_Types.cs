using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Business_Types
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Type_Name { get; set; } = default!;
        public bool Is_Deleted { get; set; }
        public bool Is_Disabled { get; set; }
        public bool Is_Checked { get; set; }
    }
}
