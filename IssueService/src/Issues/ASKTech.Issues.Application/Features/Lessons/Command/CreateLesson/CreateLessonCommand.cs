using ASKTech.Core.Abstractions;
using FileService.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Application.Features.Lessons.Command.CreateLesson
{
    public record CreateLessonCommand(
     Guid ModuleId,
     string Title,
     string Description,
     int Experience,
     IEnumerable<Guid> Tags,
     IEnumerable<Guid> Issues,
     CompleteMultipartUploadRequest MultipartRequest) : ICommand;
}
