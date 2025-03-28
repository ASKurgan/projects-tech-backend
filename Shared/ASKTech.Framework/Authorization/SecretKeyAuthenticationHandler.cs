using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ASKTech.Framework.Authorization
{
    public class SecretKeyAuthenticationHandler : AuthenticationHandler<SecretKeyAuthenticationOptions>
    {
        public SecretKeyAuthenticationHandler(
            IOptionsMonitor<SecretKeyAuthenticationOptions> options,
            ILoggerFactory logger,
        UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(Options.HeaderName, out var receivedKey))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (receivedKey != Options.ExpectedKey)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid secret key"));
            }

            var claimsIdentity = new ClaimsIdentity("SecretKey");
            claimsIdentity.AddClaim(new Claim("IsService", "true"));
            var principal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
