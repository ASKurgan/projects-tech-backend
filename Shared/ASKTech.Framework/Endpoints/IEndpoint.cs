using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace ASKTech.Framework.Endpoints
{
    public interface IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app);
    }
}
