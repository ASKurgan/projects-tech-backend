using ASKTech.Issues.Domain.Module.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.Module
{
    public interface IPositionable
    {
        Position Position { get; }

        void SetPosition(Position position);
    }
}
