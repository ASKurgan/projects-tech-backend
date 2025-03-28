using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Logging
{
    public class ElasticOptions
    {
        public const string ELASTIC = "Elastic";

        public string ElasticEndpoint { get; init; } = string.Empty;
    }
}
