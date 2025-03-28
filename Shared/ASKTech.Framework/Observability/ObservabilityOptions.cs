using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Observability
{
    public class ObservabilityOptions
    {
        public const string OBSERVABILITY = "Observability";

        public string ServiceName { get; init; } = string.Empty;

        public string OltpEndpoint { get; init; } = string.Empty;
    }
}
