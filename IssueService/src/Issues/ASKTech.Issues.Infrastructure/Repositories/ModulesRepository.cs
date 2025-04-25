using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using ASKTech.Issues.Infrastructure.DbContexts;
using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Issues.Domain.Module;
using Microsoft.EntityFrameworkCore;

namespace ASKTech.Issues.Infrastructure.Repositories
{
    public class ModulesRepository : IModulesRepository
    {
        private readonly IssuesDbContext _dbContext;

        public ModulesRepository(IssuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Module issue, CancellationToken cancellationToken = default)
        {
            await _dbContext.Modules.AddAsync(issue, cancellationToken);
            return issue.Id;
        }

        public Guid Save(Module issue, CancellationToken cancellationToken = default)
        {
            _dbContext.Modules.Attach(issue);
            return issue.Id.Value;
        }

        public Guid Delete(Module issue)
        {
            _dbContext.Modules.Remove(issue);

            return issue.Id;
        }

        public async Task<Result<Module, Error>> GetById(
            ModuleId moduleId,
            CancellationToken cancellationToken = default)
        {
            var module = await _dbContext.Modules
                .FirstOrDefaultAsync(m => m.Id == moduleId, cancellationToken);

            if (module is null)
                return Errors.General.NotFound(moduleId);

            return module;
        }

        public async Task<Result<Module, Error>> GetByTitle(
            Title title, CancellationToken cancellationToken = default)
        {
            var module = await _dbContext.Modules
                .FirstOrDefaultAsync(m => m.Title == title, cancellationToken);

            if (module is null)
                return Errors.General.NotFound();

            return module;
        }
    }
}
