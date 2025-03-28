using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Core.Messaging
{
    public interface IMessageQueue<TMessage>
    {
        Task WriteAsync(TMessage message, CancellationToken cancellationToken = default);

        Task<TMessage> ReadAsync(CancellationToken cancellationToken = default);
    }
}
