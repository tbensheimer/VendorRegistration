using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Data.Models;

namespace VendorRegistration.Services.Data.NotificationsHistoryService
{
    public interface INotificationsHistoryService
    {
        List<Notifications> GetNotificationsHistory();
        double GetNotificationsHistoryCount();
        Notifications GetNotificationById(int notifId);
        void DbAddNotification(Notifications notification);
        void RemoveNotifsAfter5Years();
    }
}
