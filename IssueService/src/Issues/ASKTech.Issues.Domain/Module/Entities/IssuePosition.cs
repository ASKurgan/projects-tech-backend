using ASKTech.Issues.Domain.Module.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.Module.Entities
{
    public class IssuePosition : Entity<IssuePositionId>, IPositionable
    {
        // ef core
        private IssuePosition()
        {
        }

        public IssuePosition(IssuePositionId id, IssueId issueId, Position position)
            : base(id)
        {
            IssueId = issueId;
            Position = position;
        }

        public IssueId IssueId { get; private set; }

        public Position Position { get; private set; }

        public void SetPosition(Position newPosition)
        {
            Position = newPosition;
        }
    }
}
