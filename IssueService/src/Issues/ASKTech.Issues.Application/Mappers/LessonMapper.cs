using ASKTech.Issues.Domain.Lesson;
using ASKTech.Issues.Domain.Module.ValueObjects;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Issues.Contracts.Lesson;

namespace ASKTech.Issues.Application.Mappers
{
    public static class LessonMapper
    {
        public static LessonDto ToDto(
            this Lesson lesson,
            Dictionary<LessonId, Position>? positions,
            Dictionary<Video, string>? videoUrls) =>
            new()
            {
                Id = lesson.Id.Value,
                ModuleId = lesson.ModuleId,
                Title = lesson.Title.Value,
                Description = lesson.Description.Value,
                Experience = lesson.Experience.Value,
                VideoUrl = videoUrls?[lesson.Video] ?? string.Empty,
                Position = positions?[lesson.Id] ?? 0,

                // TODO
                PreviewUrl = string.Empty,
                Tags = [],
                Issues = [],
            };
    }
}
