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
    public class LessonPosition : Entity<LessonPositionId>, IPositionable
    {
        // ef core
        private LessonPosition()
        {
        }

        public LessonPosition(LessonPositionId id, LessonId lessonId, Position position)
            : base(id)
        {
            LessonId = lessonId;
            Position = position;
        }

        public LessonId LessonId { get; private set; }

        public Position Position { get; private set; }

        public void SetPosition(Position position)
        {
            Position = position;
        }
    }
}
