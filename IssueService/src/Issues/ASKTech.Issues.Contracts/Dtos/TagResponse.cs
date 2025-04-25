using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Dtos
{
    public class TagResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;
    }
}
