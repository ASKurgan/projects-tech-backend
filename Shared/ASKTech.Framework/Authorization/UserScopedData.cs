using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Authorization
{
    public class UserScopedData
    {
        public Guid UserId { get; set; }

        public List<string> Permissions { get; set; } = [];

        public List<string> Roles { get; set; } = [];
    }
}
