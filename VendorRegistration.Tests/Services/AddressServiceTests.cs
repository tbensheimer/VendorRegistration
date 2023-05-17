using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.AddressService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class AddressServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static AddressService addressService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            addressService = new AddressService(_db);

            _db.Addresses.AddRange(new[]
            {
                new Address {Id = 1, CompanyId = 1, Address_1 = "123 Bee Road", Address_2 = null, City = "Greenwood", State_Region_Providence = "Indiana", Postal_Code = "46143", Country = "USA", International = false},
                new Address {Id = 2, CompanyId = 2, Address_1 = "12345 Bee Road", Address_2 = null, City = "Greenwood", State_Region_Providence = "Indiana", Postal_Code = "46143", Country = "USA", International = false},
                new Address {Id = 3, CompanyId = 3, Address_1 = "12345678 Bee Road", Address_2 = null, City = "Greenwood", State_Region_Providence = "Indiana", Postal_Code = "46143", Country = "USA", International = true}
            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllAddresses()
        {
            var addressList = addressService.GetAddressList();

            Assert.NotNull(addressList);
            Assert.That(addressList.Count, Is.EqualTo(3));
            Assert.That(addressList[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void ShouldGetAddressFromCompanyId()
        {
            var address = addressService.GetAddressFromCompanyId(3);

            Assert.NotNull(address);
            Assert.That(address.International, Is.EqualTo(true));
            Assert.That(address.Address_1, Is.EqualTo("12345678 Bee Road"));
        }

        [Test]
        public void ShouldAddAddress()
        {
            Address addressToAdd = new Address { Id = 4, CompanyId = 4, Address_1 = "111 Fry Road", Address_2 = null, City = "Indianapolis", Country = "USA", International = false, Postal_Code = "45545", State_Region_Providence = "IN" };

            addressService.DbAddAddress(addressToAdd);

            var addressList = addressService.GetAddressList();

            var address = addressService.GetAddressFromCompanyId(4);

            Assert.NotNull(addressList);
            Assert.That(addressList.Count, Is.EqualTo(4));

            Assert.NotNull(address);
            Assert.That(address.Address_1, Is.EqualTo("111 Fry Road"));
        }

        [Test]
        public void ShouldUpdateAddress()
        {
            var address = addressService.GetAddressFromCompanyId(1);

            address.Address_1 = "Updated Bee Road";

            addressService.DbUpdateAddress(address);

            var updatedAddress = addressService.GetAddressFromCompanyId(1);

            Assert.NotNull(updatedAddress);
            Assert.That(updatedAddress.Address_1, Is.EqualTo("Updated Bee Road"));
            Assert.That(updatedAddress.Id, Is.EqualTo(1));
        }


    }
}
