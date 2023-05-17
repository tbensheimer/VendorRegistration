using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Email_Attachments
    {
        [Required]
        public int Id { get; set; }
        public int Notifications_Id { get; set; }
        [Required]
        public string? File_Name { get; set; } = default!;
        [Required]
        public string? Base_64_String { get; set; } = default!;
        [Required]
        public string? Content_Type { get; set; } = default!;
    }
}
