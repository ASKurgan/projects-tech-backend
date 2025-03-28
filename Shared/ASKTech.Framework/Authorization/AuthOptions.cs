using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Authorization
{
    public class AuthOptions
    {
        public const string AUTH = nameof(AUTH);

        public string PrivateKeyPath { get; init; } = string.Empty;

        public string PublicKeyPath { get; init; } = string.Empty;

        public bool CreateNewKeys { get; init; }

        public string ExpiredMinutesTime { get; init; } = string.Empty;

        public string SecretKey { get; init; } = string.Empty;
    }
}
