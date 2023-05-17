using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Services.Data.VendorLinksService;

namespace VendorRegistration.Services.Background_Tasks
{
    public class VendorLinksCleanupService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory scopeFactory;

        public VendorLinksCleanupService(IServiceScopeFactory ScopeFactory)
        {
            scopeFactory = ScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RemoveExpiredLinks, null, TimeSpan.Zero, TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void RemoveExpiredLinks(object? state)
        {
            using (var scope = scopeFactory.CreateAsyncScope())
            {
                var linkService = scope.ServiceProvider.GetRequiredService<VendorLinksService>();

                linkService.RemoveExpiredLinks();
            }
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

