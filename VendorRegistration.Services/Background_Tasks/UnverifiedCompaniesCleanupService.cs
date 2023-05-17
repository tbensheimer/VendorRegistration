using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorRegistration.Services.Data.CompanyService;

namespace VendorRegistration.Services.Background_Tasks
{
    public class UnverifiedCompaniesCleanupService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory scopeFactory;

        public UnverifiedCompaniesCleanupService(IServiceScopeFactory ScopeFactory)
        {
            scopeFactory = ScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RemoveUnverifiedCompanies, null, TimeSpan.Zero, TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void RemoveUnverifiedCompanies(object? state)
        {
            using (var scope = scopeFactory.CreateAsyncScope())
            {
                var companyService = scope.ServiceProvider.GetRequiredService<CompanyService>();

                companyService.UnverifiedCompaniesRemove();
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}