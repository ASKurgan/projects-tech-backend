using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Contracts.Lesson
{
    public record UpdateLessonRequest(
    Guid LessonId,
    string Title,
    string Description,
    int Experience,
    Guid VideoId,
    Guid PreviewId,
    IEnumerable<Guid> Tags,
    IEnumerable<Guid> Issues);
}
