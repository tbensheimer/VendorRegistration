using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.AddressService
{
    public interface IAddressService
    {
        List<Address> GetAddressList();
        Address GetAddressFromCompanyId(int companyId);
        void DbAddAddress(Address address);
        void DbUpdateAddress(Address address);
    }
}
