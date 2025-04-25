using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Interfaces
{
    public interface IOutboxRepository
    {
        Task Add<T>(T message, CancellationToken cancellationToken);
    }
}
