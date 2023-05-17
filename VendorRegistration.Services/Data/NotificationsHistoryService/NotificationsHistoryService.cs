using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.NotificationsHistoryService
{

    public class NotificationsHistoryService : INotificationsHistoryService
    {

        private VendorDataDbContext _db;

        public NotificationsHistoryService(VendorDataDbContext db)
        {
            _db = db;
        }

        public List<Notifications> GetNotificationsHistory()
        {
            List<Notifications> notifsList = new();

            notifsList = _db.NotificationsList.ToList();

            return notifsList;
        }

        public Notifications GetNotificationById(int notifId)
        {
            Notifications notification = _db.NotificationsList.FirstOrDefault(notif => notif.Id == notifId);

            return notification;
        }


        public void DbAddNotification(Notifications notification)
        {
            _db.NotificationsList.Add(notification);
            _db.SaveChanges();
        }

        public double GetNotificationsHistoryCount()
        {
            double count = _db.NotificationsList.Count();

            return count;
        }

        public void RemoveNotifsAfter5Years()
        {
            var notifications = _db.NotificationsList.ToArray();

            foreach (var notif in notifications)
            {
                if (DateTime.Now > notif.Date_Sent.AddYears(5))
                {
                    var attachments = _db.Email_Attachments.Where(attch => attch.Notifications_Id == notif.Id).ToArray();
                    var recipients = _db.Notification_Recipients.Where(rec => rec.NotificationsId == notif.Id).ToArray();

                    if (attachments is not null)
                    {
                        foreach (var attch in attachments)
                        {
                            _db.Email_Attachments.Remove(attch);
                        }
                    }

                    if (recipients is not null)
                    {
                        foreach (var rec in recipients)
                        {
                            _db.Notification_Recipients.Remove(rec);
                        }
                    }

                    _db.NotificationsList.Remove(notif);
                    _db.SaveChanges();
                }
            }
        }
    }
}
