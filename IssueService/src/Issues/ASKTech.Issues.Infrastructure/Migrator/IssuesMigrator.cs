using ASKTech.Issues.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMigrator = ASKTech.Core.Database.IMigrator;

namespace ASKTech.Issues.Infrastructure.Migrator
{
    public class IssuesMigrator(IssuesDbContext context, ILogger<IssuesMigrator> logger) : IMigrator
    {
        public async Task Migrate(CancellationToken cancellationToken = default)
        {
            logger.Log(LogLevel.Information, "Applying issues migrations...");

            if (await context.Database.CanConnectAsync(cancellationToken) == false)
            {
                await context.Database.EnsureCreatedAsync(cancellationToken);
            }

            await context.Database.MigrateAsync(cancellationToken);

            logger.Log(LogLevel.Information, "Migrations issues applied successfully.");
        }
    }
}
