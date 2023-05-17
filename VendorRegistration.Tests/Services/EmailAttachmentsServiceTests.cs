using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.EmailAttachmentsService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class EmailAttachmentsServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static EmailAttachmentsService attachService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            attachService = new EmailAttachmentsService(_db);

            _db.Email_Attachments.AddRange(new[]
            {
                new Email_Attachments {Id = 1, Notifications_Id = 1, File_Name = "Database.pdf", Content_Type = "pdf", Base_64_String = "baseString" },
                new Email_Attachments {Id = 2, Notifications_Id = 1, File_Name = "Database.pdf", Content_Type = "pdf", Base_64_String = "baseString" },
                new Email_Attachments {Id = 3, Notifications_Id = 2, File_Name = "Database.pdf", Content_Type = "pdf", Base_64_String = "baseString" },

            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllEmailAttachments()
        {
            var attachList = attachService.GetAllEmailAttachments();

            Assert.NotNull(attachList);
            Assert.That(attachList.Count, Is.EqualTo(3));
            Assert.That(attachList[2].Notifications_Id, Is.EqualTo(2));
        }

        [Test]
        public void ShouldAddEmailAttachment()
        {
            Email_Attachments attch = new Email_Attachments { Id = 4, Notifications_Id = 3, File_Name = "reciept.pdf", Content_Type = "pdf", Base_64_String = "baseString" };

            attachService.DbAddEmailAttachment(attch);

            var attachList = attachService.GetAllEmailAttachments();

            Assert.NotNull(attachList);
            Assert.That(attachList.Count, Is.EqualTo(4));
            Assert.That(attachList[3].Notifications_Id, Is.EqualTo(3));
        }
    }
}
