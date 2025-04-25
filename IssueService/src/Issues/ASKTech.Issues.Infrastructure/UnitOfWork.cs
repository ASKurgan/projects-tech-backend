using ASKTech.Core.Database;
using ASKTech.Issues.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IssuesDbContext _dbContext;

        public UnitOfWork(IssuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            return transaction.GetDbTransaction();
        }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
