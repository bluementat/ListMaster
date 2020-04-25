using ListMaster.Server.Data;
using ListMaster.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ListMaster.Server
{
    public class SeedingHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public SeedingHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var seeder = new UserInitializer();                
                await seeder.SeedData(
                    scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>(),
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>(),
                    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
