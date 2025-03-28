using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Authorization
{
    public class SecretKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string HeaderName { get; init; } = "X-Internal-Service-Key";

        public string ExpectedKey { get; set; } = string.Empty;
    }
}
