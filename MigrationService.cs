using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mtd.Cpq.Manager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mtd.Cpq.Manager
{
    public class MigrationService : IHostedService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger logger;

        public MigrationService(IServiceScopeFactory serviceScopeFactory,
            ILogger<MigrationService> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<CpqContext>();

            bool dbMigration = false;
 
            try
            {
                IEnumerable<string> pm = await dbContext.Database
                    .GetPendingMigrationsAsync(cancellationToken);
                dbMigration = pm.Any();

                if (dbMigration)
                {
                    await dbContext.Database.MigrateAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


    }
}
