using MediatR;
using Mentoroom.DOMAIN.Interfaces.Shared;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.DeleteCourse
{
    public class DeleteCourseHandler(ICourseRepository courseRepository) : IRequestHandler<DeleteCourse>
    {
        private readonly ICourseRepository courseRepository = courseRepository;

        public async Task Handle(DeleteCourse request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseById(request.CourseId);
            if (course == null)
            {
                throw new Exception($"This course does not exist");
            }
            await courseRepository.DeleteCourseById(course);
        }
    }
}
