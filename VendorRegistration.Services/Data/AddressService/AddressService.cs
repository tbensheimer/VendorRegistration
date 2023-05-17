using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.AddressService
{
    public class AddressService : IAddressService
    {
        private VendorDataDbContext _db;

        public AddressService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Address> GetAddressList()
        {
            List<Address> addressList = new();

            addressList = _db.Addresses.ToList();

            return addressList;
        }

        public Address GetAddressFromCompanyId(int companyId)
        {
            var address = _db.Addresses.FirstOrDefault(address => address.CompanyId == companyId);

            return address;
        }

        public void DbAddAddress(Address address)
        {
            _db.Addresses.Add(address);
            _db.SaveChanges();
        }

        public void DbUpdateAddress(Address address)
        {
            _db.Addresses.Update(address);
            _db.SaveChanges();
        }
    }
}

