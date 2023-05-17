using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;
using VendorRegistration.Services.Data.NotificationsHistoryService;

namespace VendorRegistration.Tests.Services
{
    [TestFixture]
    public class NotificationsHistoryServiceTests
    {
        private static VendorDataDbContext _db;
        private static DbContextOptions<VendorDataDbContext> options;
        private static NotificationsHistoryService notifService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<VendorDataDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _db = new VendorDataDbContext(options);
            _db.Database.EnsureCreated();

            notifService = new NotificationsHistoryService(_db);

            _db.NotificationsList.AddRange(new[]
            {
                new Notifications { Id = 1, Title = "Need Supplies", Date_Sent = DateTime.Now, Body = "hello 1" },
                new Notifications { Id = 2, Title = "Need Supplies Wood", Date_Sent = DateTime.Now, Body = "hello 2" },
                new Notifications { Id = 3, Title = "Need Supplies Steel", Date_Sent = DateTime.Now, Body = "hello 3" },
                new Notifications { Id = 4, Title = "Need Supplies Gold", Date_Sent = DateTime.Now, Body = "hello 4" },
            });

            _db.SaveChanges();
        }

        [Test]
        public void ShouldGetAllNotifications()
        {
            var notifList = notifService.GetNotificationsHistory();

            Assert.NotNull(notifList);
            Assert.That(notifList.Count, Is.EqualTo(4));
            Assert.That(notifList[0].Title, Is.EqualTo("Need Supplies"));
        }

        [Test]
        public void ShouldGetNotificationsCount()
        {
            var count = notifService.GetNotificationsHistoryCount();

            Assert.NotNull(count);
            Assert.That(count, Is.EqualTo(4));
        }

        [Test]
        public void ShouldGetNotificationFromId()
        {
            var notif = notifService.GetNotificationById(4);

            Assert.NotNull(notif);
            Assert.That(notif.Id, Is.EqualTo(4));
            Assert.That(notif.Body, Is.EqualTo("hello 4"));
        }

        [Test]
        public void ShouldAddNotification()
        {
            Notifications notif = new Notifications { Id = 5, Title = "Here is Receipt", Date_Sent = DateTime.Now, Body = "hello 5" };

            notifService.DbAddNotification(notif);

            var addedNotif = notifService.GetNotificationById(5);

            var list = notifService.GetNotificationsHistory();

            Assert.NotNull(list);
            Assert.That(list.Count, Is.EqualTo(5));

            Assert.NotNull(addedNotif);
            Assert.That(addedNotif.Title, Is.EqualTo("Here is Receipt"));
        }

        [Test]
        public async Task ShouldRemoveNotifsAfter5Years()
        {

            Notifications notif1 = new Notifications { Id = 5, Title = "Here is Receipt", Date_Sent = new DateTime(2017, 1, 15, 8, 30, 00), Body = "hello 5" };
            Notifications notif2 = new Notifications { Id = 6, Title = "Here is Receipt", Date_Sent = DateTime.Now, Body = "hello 6" };
            Notifications notif3 = new Notifications { Id = 7, Title = "Here is Receipt", Date_Sent = new DateTime(1999, 1, 5, 8, 30, 00), Body = "hello 7" };

            Email_Attachments attch1 = new Email_Attachments { Id = 1, Notifications_Id = 5, File_Name = "date.pdf", Content_Type = "pdf", Base_64_String = "stringbase" };
            Email_Attachments attch2 = new Email_Attachments { Id = 2, Notifications_Id = 6, File_Name = "date.pdf", Content_Type = "pdf", Base_64_String = "stringbase" };
            Email_Attachments attch3 = new Email_Attachments { Id = 3, Notifications_Id = 7, File_Name = "date.pdf", Content_Type = "pdf", Base_64_String = "stringbase" };

            Notification_Recipient rec1 = new Notification_Recipient { Id = 1, NotificationsId = 5, Category_Id = 1, Type_Id = 1, CompanyId = 1 };
            Notification_Recipient rec2 = new Notification_Recipient { Id = 2, NotificationsId = 6, Category_Id = 1, Type_Id = 1, CompanyId = 1 };
            Notification_Recipient rec3 = new Notification_Recipient { Id = 3, NotificationsId = 7, Category_Id = 1, Type_Id = 1, CompanyId = 1 };

            _db.NotificationsList.Add(notif1);
            _db.NotificationsList.Add(notif2);
            _db.NotificationsList.Add(notif3);

            _db.Notification_Recipients.Add(rec1);
            _db.Notification_Recipients.Add(rec2);
            _db.Notification_Recipients.Add(rec3);

            _db.Email_Attachments.Add(attch1);
            _db.Email_Attachments.Add(attch2);
            _db.Email_Attachments.Add(attch3);

            _db.SaveChanges();

            notifService.RemoveNotifsAfter5Years();

            var notifList = notifService.GetNotificationsHistory();
            var recList = _db.Notification_Recipients.ToList();
            var attchList = _db.Email_Attachments.ToList();

            Assert.NotNull(notifList);
            Assert.That(notifList.Count, Is.EqualTo(5));
            Assert.That(notifList[4].Id, Is.EqualTo(6));

            Assert.NotNull(recList);
            Assert.That(recList.Count, Is.EqualTo(1));
            Assert.That(recList[0].NotificationsId, Is.EqualTo(6));
            Assert.That(recList[0].Id, Is.EqualTo(2));

            Assert.NotNull(attchList);
            Assert.That(attchList.Count, Is.EqualTo(1));
            Assert.That(attchList[0].Notifications_Id, Is.EqualTo(6));
            Assert.That(attchList[0].Id, Is.EqualTo(2));
        }
    }
}
