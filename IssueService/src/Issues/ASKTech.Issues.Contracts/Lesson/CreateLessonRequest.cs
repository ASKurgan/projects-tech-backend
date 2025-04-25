using FileService.Contracts;
namespace ASKTech.Issues.Contracts.Lesson
{
    public record CreateLessonRequest(
     Guid ModuleId,
     string Title,
     string Description,
     int Experience,
     IEnumerable<Guid> Tags,
     IEnumerable<Guid> Issues,
     CompleteMultipartUploadRequest MultipartRequest);
}
