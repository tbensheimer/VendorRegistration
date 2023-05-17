using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.VendorLinksService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class VendorLinksServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static VendorLinksService linkService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            linkService = new VendorLinksService(_db);

            _db.Vendor_Links.AddRange(new[]
            {
                new Vendor_Links {Id = 1, CompanyId = 1, DateLinkSent = DateTime.Now, Link = "www.link1.com"},
                new Vendor_Links {Id = 2, CompanyId = 2, DateLinkSent = DateTime.Now, Link = "www.link2.com"},
                new Vendor_Links {Id = 3, AccountId = 1, DateLinkSent = DateTime.Now, Link = "www.link3.com"},
                new Vendor_Links {Id = 4, AccountId = 2, DateLinkSent = DateTime.Now, Link = "www.link4.com"},
            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllLinks()
        {
            var links = linkService.GetVendorLinks();

            Assert.NotNull(links);
            Assert.That(links.Count, Is.EqualTo(4));
            Assert.That(links[3].Link, Is.EqualTo("www.link4.com"));
        }

        [Test]
        public void ShouldAddLink()
        {
            Vendor_Links link = new Vendor_Links { Id = 5, CompanyId = 3, DateLinkSent = DateTime.Now, Link = "www.link5.com" };

            linkService.DbAddLink(link);

            var links = linkService.GetVendorLinks();

            Assert.NotNull(links);
            Assert.That(links.Count, Is.EqualTo(5));
            Assert.That(links[4].CompanyId, Is.EqualTo(3));
            Assert.That(links[4].AccountId, Is.EqualTo(0));
        }

        [Test]
        public void ShouldRemoveLink()
        {
            var link = linkService.GetVendorLinks()[0];

            linkService.DbRemoveLink(link);

            var links = linkService.GetVendorLinks();

            Assert.NotNull(links);
            Assert.That(links.Count, Is.EqualTo(3));
            Assert.That(links[0].Id, Is.EqualTo(2));
        }

        [Test]
        public void ShouldRemoveExpiredLinks()
        {
            Vendor_Links link1 = new Vendor_Links { Id = 5, CompanyId = 3, DateLinkSent = new DateTime(2022, 2, 3, 8, 30, 00), Link = "www.link5.com" };
            Vendor_Links link2 = new Vendor_Links { Id = 6, CompanyId = 3, DateLinkSent = DateTime.Now, Link = "www.link5.com" };
            Vendor_Links link3 = new Vendor_Links { Id = 7, CompanyId = 3, DateLinkSent = new DateTime(2022, 12, 20, 8, 30, 00), Link = "www.link5.com" };

            _db.Vendor_Links.Add(link1);
            _db.Vendor_Links.Add(link2);
            _db.Vendor_Links.Add(link3);
            _db.SaveChanges();

            linkService.RemoveExpiredLinks();

            var list = linkService.GetVendorLinks();

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(5));
            Assert.That(list[4].Id, Is.EqualTo(6));
        }
    }
}
