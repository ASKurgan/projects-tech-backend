using ASKTech.Issues.Domain.Issue.Events;
using ASKTech.Issues.Domain.Issue.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
// using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASKTech.Issues.Domain.Issue
{
    public class Issue : DomainEntity<IssueId>, ISoftDeletable
    {
        private List<FileId> _files = [];

        // ef core
        private Issue(IssueId id)
            : base(id)
        {
        }

        public Issue(
            IssueId id,
        Title title,
            Description description,
            LessonId? lessonId,
            ModuleId moduleId,
            Experience experience,
            IEnumerable<FileId>? files = null)
            : base(id)
        {
            Title = title;
            Description = description;
            LessonId = lessonId;
            ModuleId = moduleId;
            Experience = experience;
            _files = files?.ToList() ?? [];

            AddDomainEvent(new IssueCreatedEvent(id, moduleId));
        }

        public Experience Experience { get; private set; } = default!;

        public Title Title { get; private set; } = default!;

        public Description Description { get; private set; } = default!;

        public LessonId? LessonId { get; private set; }

        public ModuleId ModuleId { get; private set; } = null!;

        public IReadOnlyList<FileId> Files => _files;

        public bool IsDeleted { get; private set; }

        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletionDate = null;
        }

        public void UpdateFiles(IEnumerable<FileId> files)
        {
            _files = files.ToList();
        }

        public UnitResult<Error> UpdateMainInfo(
        Title title,
            Description description,
            LessonId? lessonId,
            ModuleId moduleId,
            Experience experience)
        {
            Title = title;
            Description = description;
            LessonId = lessonId;
            ModuleId = moduleId;
            Experience = experience;

            return Result.Success<Error>();
        }
    }
}
