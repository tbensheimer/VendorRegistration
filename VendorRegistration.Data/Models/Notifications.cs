using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Notifications
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; } = default!;
        [Required]
        public string? Body { get; set; } = default!;
        [Required]
        public DateTime Date_Sent { get; set; }
        public string? Created_By { get; set; } = default!;
    }
}
