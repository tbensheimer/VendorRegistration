using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.FunctionsService.RegisterService
{
    public interface IRegisterService
    {
        void CreateVendorAccount(Company Company, VendorAccount Account, Address Address, List<Company_Types_Categories> CompTypesCats, bool admin, Vendor_Links vendorLinks, List<Authorized_Contacts> ContactsList);
    }
}
