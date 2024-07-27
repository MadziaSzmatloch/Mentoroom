using MediatR;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Queries.DownloadStudentFilesByCourse
{
    public class DownloadStudentFilesByCourseHandler(IFileRepository fileRepository, ICourseRepository courseRepository) : IRequestHandler<DownloadStudentFilesByCourse, DOMAIN.Models.Blob>
    {
        private readonly IFileRepository fileRepository = fileRepository;
        private readonly ICourseRepository courseRepository = courseRepository;

        public async Task<DOMAIN.Models.Blob> Handle(DownloadStudentFilesByCourse request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseById(request.CourseId);
            if (course == null)
            {
                throw new Exception("There is no course with this id");
            }
            var list = await fileRepository.BlobList();
            if (list.Any(b => b.Name.Contains($"studentfiles/{course.Name}")) == false)
            {
                throw new Exception("This course does not have any files");
            }
            var result = await fileRepository.DownloadFolder($"studentfiles/{course.Name}");
            if (result == null)
            {
                throw new Exception("Something went wrong");
            }
            return result;
        }
    }
}
