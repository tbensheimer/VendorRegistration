using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Vendor_Links
    {
        [Required]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CompanyId { get; set; }
        [Required]
        public string? Link { get; set; }
        [Required]
        public DateTime DateLinkSent { get; set; }
    }
}
