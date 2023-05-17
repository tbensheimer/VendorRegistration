using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Models.Authentication;

namespace VendorRegistration.Services.Authentication
{
    public interface IUserService
    {
        User ValidateUser(string userName, string password, string url, string passcode);
    }
}
