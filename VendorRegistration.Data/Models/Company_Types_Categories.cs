using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Company_Types_Categories
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int Category_Id { get; set; }
        [Required]
        public int Type_Id { get; set; }
    }
}
