using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace VendorRegistration.Services.Authentication
{
    public class VendorClaimsPrincipal : IVendorClaimsPrincipal
    {
        private ClaimsPrincipal claimsPrincipal;

        public ClaimsPrincipal GetCurrentClaimsPrincipal()
        {
            return claimsPrincipal;
        }

        public void StoreClaimsPrincipal(ClaimsPrincipal principal)
        {
            claimsPrincipal = principal;
        }

        public void LogoutClaimsPrincipal()
        {
            claimsPrincipal = null;
        }
    }
}
