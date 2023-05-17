using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.NotificationRecipientsService
{
    public interface INotificationRecipientsService
    {
        List<Notification_Recipient> GetAllNotificationRecipients();
        List<Notification_Recipient> GetNotificationsFromNotifId(int notifId);
        void DbAddRecipient(Notification_Recipient recipient);
    }
}
