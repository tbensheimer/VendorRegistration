using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Services.Data.NotificationsHistoryService;

namespace VendorRegistration.Services.Background_Tasks
{
    public class NotificationsHistoryCleanupService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory scopeFactory;

        public NotificationsHistoryCleanupService(IServiceScopeFactory ScopeFactory)
        {
            scopeFactory = ScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RemoveNotificationsAfterFiveYears, null, TimeSpan.Zero, TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void RemoveNotificationsAfterFiveYears(object? state)
        {
            using (var scope = scopeFactory.CreateAsyncScope())
            {
                var notifService = scope.ServiceProvider.GetRequiredService<NotificationsHistoryService>();

                notifService.RemoveNotifsAfter5Years();
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
