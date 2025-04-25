using ASKTech.Issues.Domain.Lesson;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace ASKTech.Issues.Application.Interfaces
{
    public interface ILessonsRepository
    {
        public Task<Guid> Add(Lesson lesson, CancellationToken cancellationToken = default);

        public Guid Save(Lesson module);

        public Guid Delete(Lesson module);

        public Task<Result<Lesson, Error>> GetById(LessonId lessonId, CancellationToken cancellationToken = default);

        public Task<Result<Lesson, Error>> GetByTitle(Title title, CancellationToken cancellationToken = default);
    }
}
