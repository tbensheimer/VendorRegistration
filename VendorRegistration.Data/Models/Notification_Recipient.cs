using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Data.Models
{
    public class Notification_Recipient
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int NotificationsId { get; set; }
        public int Type_Id { get; set; }
        public int Category_Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
    }
}
