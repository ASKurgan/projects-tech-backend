using ASKTech.Framework.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Http
{
    public class HttpTrackerHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _context;
        private readonly AuthOptions _authOptions;

        public HttpTrackerHandler(IHttpContextAccessor context, IOptions<AuthOptions> authOptions)
        {
            _context = context;
            _authOptions = authOptions.Value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            const string authKey = "X-Internal-Service-Key";
            const string authorization = "Authorization";

            if (_context.HttpContext == null ||
                !_context.HttpContext.Request.Headers.TryGetValue(authorization, out var jwtValues) ||
                string.IsNullOrWhiteSpace(jwtValues.FirstOrDefault()))
            {
                request.Headers.Add(authKey, _authOptions.SecretKey);
            }
            else
            {
                request.Headers.Add(authorization, jwtValues.First());
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
