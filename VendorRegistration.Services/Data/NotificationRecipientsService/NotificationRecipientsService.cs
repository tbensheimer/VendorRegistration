using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.NotificationRecipientsService
{
    public class NotificationRecipientsService : INotificationRecipientsService
    {
        private VendorDataDbContext _db;

        public NotificationRecipientsService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Notification_Recipient> GetAllNotificationRecipients()
        {
            List<Notification_Recipient> recipientList;

            recipientList = _db.Notification_Recipients.ToList();

            return recipientList;
        }

        public List<Notification_Recipient> GetNotificationsFromNotifId(int notifId)
        {
            List<Notification_Recipient> recipientList = new();

            recipientList = _db.Notification_Recipients.Where(rec => rec.NotificationsId == notifId).ToList();

            return recipientList;
        }

        public void DbAddRecipient(Notification_Recipient recipient)
        {
            _db.Notification_Recipients.Add(recipient);
            _db.SaveChanges();
        }
    }
}
