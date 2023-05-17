using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.AccountService;
using VendorRegistration.Services.Data.CompanyService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static CompanyService companyService;
        private static AccountService accountService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();
            accountService = new AccountService(_db);

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);

            _db.Accounts.AddRange(new[]
           {
                new VendorAccount {Id = 1, CompanyId = 1, Email = "trentco@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("trent123", salt)},
                new VendorAccount {Id = 2, CompanyId = 2, Email = "airplane@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("airplane123", salt)},
                new VendorAccount {Id = 3, CompanyId = 5, Email = "foods@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("airplane123", salt)}
            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllAccounts()
        {
            var accountList = accountService.GetVendorAccounts();

            Assert.NotNull(accountList);
            Assert.That(accountList.Count, Is.EqualTo(3));
            Assert.That(accountList[0].Email, Is.EqualTo("trentco@gmail.com"));
        }

        [Test]
        public void ShouldGetAccountById()
        {
            var account = accountService.GetAccountById(2);
            bool isMatch = BCrypt.Net.BCrypt.Verify("airplane123", account.Password);

            Assert.NotNull(account);
            Assert.That(account.CompanyId, Is.EqualTo(2));
            Assert.That(isMatch, Is.EqualTo(true));
        }

        [Test]
        public void ShouldGetAllAccountsByUsername()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);

            VendorAccount account = new VendorAccount { Id = 4, CompanyId = 5, Email = "foods@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("airplane123", salt) };

            _db.Accounts.Add(account);
            _db.SaveChanges();

            var accountList = accountService.GetAccountsByUsername("foods@gmail.com");

            Assert.NotNull(accountList);
            Assert.That(accountList.Count, Is.EqualTo(2));
            Assert.That(accountList[0].Id, Is.EqualTo(3));
        }

        [Test]
        public void ShouldGetAccountByCompanyId()
        {
            var account = accountService.GetAccountByCompanyId(1);

            Assert.NotNull(account);
            Assert.That(account.Id, Is.EqualTo(1));
            Assert.That(account.Email, Is.EqualTo("trentco@gmail.com"));
        }

        [Test]
        public void ShouldAddAccount()
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);

            VendorAccount account = new VendorAccount { Id = 4, CompanyId = 1, Email = "foods@gmail.com", Password = BCrypt.Net.BCrypt.HashPassword("airplane123", salt) };

            accountService.DbAddAccount(account);

            var accountList = accountService.GetVendorAccounts();

            Assert.NotNull(accountList);
            Assert.That(accountList.Count, Is.EqualTo(4));
            Assert.That(accountList[3].Email, Is.EqualTo("foods@gmail.com"));
        }

        [Test]
        public void ShouldUpdateAcount()
        {
            var account = accountService.GetAccountById(1);

            account.Email = "trent123@gmail.com";

            accountService.DbUpdateAccount(account);

            var updatedAccount = accountService.GetAccountById(1);

            Assert.NotNull(updatedAccount);
            Assert.That(updatedAccount.Email, Is.EqualTo("trent123@gmail.com"));
        }

    }
}
