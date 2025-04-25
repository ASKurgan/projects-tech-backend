using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.Lesson;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using ASKTech.Issues.Infrastructure.DbContexts;
using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASKTech.Issues.Infrastructure.Repositories
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly IssuesDbContext _dbContext;

        public LessonsRepository(IssuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Lesson lesson, CancellationToken cancellationToken = default)
        {
            await _dbContext.Lessons.AddAsync(lesson, cancellationToken);
            return lesson.Id;
        }

        public Guid Save(Lesson module)
        {
            _dbContext.Lessons.Attach(module);
            return module.Id.Value;
        }

        public Guid Delete(Lesson module)
        {
            _dbContext.Lessons.Remove(module);

            return module.Id;
        }

        public async Task<Result<Lesson, Error>> GetById(
            LessonId lessonId, CancellationToken cancellationToken = default)
        {
            var module = await _dbContext.Lessons
                .FirstOrDefaultAsync(m => m.Id == lessonId, cancellationToken);

            if (module is null)
                return Errors.General.NotFound(lessonId);

            return module;
        }

        public async Task<Result<Lesson, Error>> GetByTitle(
            Title title, CancellationToken cancellationToken = default)
        {
            var module = await _dbContext.Lessons
                .FirstOrDefaultAsync(m => m.Title == title, cancellationToken);

            if (module is null)
                return Errors.General.NotFound();

            return module;
        }
    }
}
