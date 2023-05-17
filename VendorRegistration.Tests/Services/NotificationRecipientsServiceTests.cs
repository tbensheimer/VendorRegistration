using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.NotificationRecipientsService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class NotificationRecipientsServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static NotificationRecipientsService recService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            recService = new NotificationRecipientsService(_db);

            _db.Notification_Recipients.AddRange(new[]
            {
                new Notification_Recipient {Id = 1, NotificationsId = 1, CompanyId = 1, Type_Id = 1, Category_Id = 1},
                new Notification_Recipient {Id = 2, NotificationsId = 1, CompanyId = 2, Type_Id = 1, Category_Id = 1},
                new Notification_Recipient {Id = 3, NotificationsId = 2, CompanyId = 2, Type_Id = 1, Category_Id = 1},
                new Notification_Recipient {Id = 4, NotificationsId = 3, CompanyId = 3, Type_Id = 2, Category_Id = 1},

            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllNotifRecipients()
        {
            var recList = recService.GetAllNotificationRecipients();

            Assert.NotNull(recList);
            Assert.That(recList.Count, Is.EqualTo(4));
            Assert.That(recList[1].CompanyId, Is.EqualTo(2));
        }

        [Test]
        public void ShouldGetAllNotifRecipientsFromNotifId()
        {
            var recList = recService.GetNotificationsFromNotifId(1);

            Assert.NotNull(recList);
            Assert.That(recList.Count, Is.EqualTo(2));
            Assert.That(recList[0].Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddRecipient()
        {
            Notification_Recipient rec = new Notification_Recipient { Id = 5, NotificationsId = 4, Type_Id = 3, Category_Id = 1 };

            recService.DbAddRecipient(rec);

            var list = recService.GetAllNotificationRecipients();

            var addedRec = recService.GetNotificationsFromNotifId(4);

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(5));

            Assert.NotNull(addedRec);
            Assert.That(addedRec[0].Type_Id, Is.EqualTo(3));
        }
    }
}
