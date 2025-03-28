using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Core.Abstractions
{
    public interface ICommandHandler<TResponse, in TCommand>
    where TCommand : ICommand
    {
        public Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        public Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellationToken = default);
    }
}
