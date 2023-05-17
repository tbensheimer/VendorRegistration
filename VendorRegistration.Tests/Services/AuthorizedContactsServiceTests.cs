using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.AuthorizedContactsService;
using VendorRegistration.Services.Data.CompanyService;
using VendorRegistration.Services.FunctionsService.EncryptionService;

namespace VendorRegistration.Tests.Services
{
    public class AuthorizedContactsServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static CompanyService companyService;
        private static AuthorizedContactsService contactService;
        private static EncryptionService encryptionService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();
            encryptionService = new EncryptionService();
            contactService = new AuthorizedContactsService(_db, encryptionService);
            companyService = new CompanyService(_db);

            _db.Authorized_Contacts.AddRange(new[]
        {
                new Authorized_Contacts { Id = 1, CompanyId = 1, FirstName = "Joe", LastName = "Ben", Email = "trent1@gmail.com", Phone_Number = 3333333333, Contact_Deleted = false },
                new Authorized_Contacts { Id = 2, CompanyId = 1, FirstName = "Mark", LastName = "Ben", Email = "trent2@gmail.com", Phone_Number = 3333333333, Contact_Deleted = false },
                new Authorized_Contacts { Id = 3, CompanyId = 2, FirstName = "Bill", LastName = "Ben", Email = "trent3@gmail.com", Phone_Number = 3333333333, Contact_Deleted = false },
                new Authorized_Contacts { Id = 4, CompanyId = 3, FirstName = "Scott", LastName = "Ben", Email = "trent4@gmail.com", Phone_Number = 3333333333, Contact_Deleted = false },
                new Authorized_Contacts { Id = 5, CompanyId = 3, FirstName = "Scottttty", LastName = "Ben", Email = "trent4@gmail.com", Phone_Number = 3333333333, Contact_Deleted = true }
            });

            _db.Companies.AddRange(new[]
            {
                new Company { Id = 1, Name = "TrentCo", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111111", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "City of Indianapolis", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = false, Completed_Registeration = true, EditedSince = DateTime.Now, Minority = true },
                new Company { Id = 2, Name = "Airplane", Sign_Up = DateTime.Now, Fed_Id_SSN = "111111112", Supplier_Category = "United States Entity", Website = null, Diversity_Certification = "Not Certified", Other_Diversity_Certification = null, Description_Goods_Services = "Trent's Goods", Disabled_From_Notifications = false, Is_Deleted = false, Completed_Registeration = true, EditedSince = new DateTime(2022, 12, 5, 8, 30, 0), Minority = false },
            });

            _db.SaveChanges();
        }


        [Test]
        public void ShouldGetAllContacts()
        {
            var contactList = contactService.GetAuthorizedContactsList();

            Assert.NotNull(contactList);
            Assert.That(contactList.Count, Is.EqualTo(5));
            Assert.That(contactList[4].Contact_Deleted, Is.EqualTo(true));
        }

        [Test]
        public void ShouldGetContactsFromCompanyId()
        {
            var contactList = contactService.GetContactsFromCompanyId(3);

            Assert.NotNull(contactService);
            Assert.That(contactList.Count, Is.EqualTo(1));
            Assert.That(contactList[0].Contact_Deleted, Is.EqualTo(false));
            Assert.That(contactList[0].Id, Is.EqualTo(4));
        }

        [Test]
        public void ShouldGetContactFromId()
        {
            var contact = contactService.GetContactFromId(2);

            Assert.NotNull(contact);
            Assert.That(contact.FirstName, Is.EqualTo("Mark"));
            Assert.That(contact.CompanyId, Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddContact()
        {
            Authorized_Contacts contact = new Authorized_Contacts { Id = 6, CompanyId = 1, FirstName = "Sco", LastName = "Bern", Email = "trent4@gmail.com", Phone_Number = 3333333333, Contact_Deleted = false };

            var company = companyService.GetCompanyById(1);
            contactService.DbAddContact(contact, company);

            var contactList = contactService.GetAuthorizedContactsList();

            Assert.NotNull(contactList);
            Assert.That(contactList.Count, Is.EqualTo(6));
            Assert.That(contactList[5].FirstName, Is.EqualTo("Sco"));
        }

        [Test]
        public void ShouldAddRangeContacts()
        {
            List<Authorized_Contacts> contactListToAdd = new();

            Authorized_Contacts contact1 = new Authorized_Contacts { Id = 6, CompanyId = 1, FirstName = "Sco", LastName = "Bern", Email = "trent4@gmail.com", Phone_Number = 3333333333, Contact_Deleted = false };
            Authorized_Contacts contact2 = new Authorized_Contacts { Id = 7, CompanyId = 1, FirstName = "Sco", LastName = "Bern", Email = "trent4@gmail.com", Phone_Number = 3333333333, Contact_Deleted = false };
            List<Authorized_Contacts> existingcontacts = contactService.GetContactsFromCompanyId(1);

            foreach (var contact in existingcontacts)
            {
                contactListToAdd.Add(contact);
            }
            contactListToAdd.Add(contact1);
            contactListToAdd.Add(contact2);

            contactService.DbAddRangeContacts(contactListToAdd, 1);

            var contactList = contactService.GetAuthorizedContactsList();

            Assert.NotNull(contactList);
            Assert.That(contactList.Count, Is.EqualTo(7));
            Assert.That(contactList[5].Id, Is.EqualTo(7));
        }

        [Test]
        public void ShouldUpdateContact()
        {
            var contact = contactService.GetContactFromId(1);

            contact.Email = "joe@gmail.com";

            contactService.DbUpdateContact(contact);

            var updatedContact = contactService.GetContactFromId(1);

            Assert.NotNull(updatedContact);
            Assert.That(updatedContact.Email, Is.EqualTo("joe@gmail.com"));
            Assert.That(updatedContact.Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldRemoveContact()
        {
            var list = contactService.GetContactsFromCompanyId(1);

            foreach (var contact in list)
            {
                contactService.DbRemoveContact(contact);
            }

            var updatedList = contactService.GetAuthorizedContactsList();

            Assert.NotNull(updatedList);
            Assert.That(updatedList.Count, Is.EqualTo(3));
            Assert.That(updatedList[2].Id, Is.EqualTo(5));
        }
    }
}
