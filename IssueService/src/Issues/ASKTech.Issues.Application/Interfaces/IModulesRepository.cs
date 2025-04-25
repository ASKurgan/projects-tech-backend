using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Issues.Domain.Module;
using SharedKernel;

namespace ASKTech.Issues.Application.Interfaces
{
    public interface IModulesRepository
    {
        Task<Guid> Add(Module issue, CancellationToken cancellationToken = default);

        Guid Save(Module issue, CancellationToken cancellationToken = default);

        Guid Delete(Module issue);

        Task<Result<Module, Error>> GetById(ModuleId moduleId, CancellationToken cancellationToken = default);

        Task<Result<Module, Error>> GetByTitle(Title title, CancellationToken cancellationToken = default);
    }
}
