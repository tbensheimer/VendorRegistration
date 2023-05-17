using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Models.Authentication
{
    public class User
    {
        public string? UserName { get; set; }
        public string? Domain { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public string? Company { get; set; }
        public int CompanyId { get; set; }
    }
}
