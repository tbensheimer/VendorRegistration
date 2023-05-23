using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Services.Authentication
{
    public interface IVendorClaimsPrincipal
    {
        ClaimsPrincipal GetCurrentClaimsPrincipal();
        void StoreClaimsPrincipal(ClaimsPrincipal principal);
        void LogoutClaimsPrincipal();
    }
}
