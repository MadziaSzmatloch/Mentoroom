using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.Course.Queries.GetCourse
{
    public class GetCourseHandler(ICourseRepository courseRepository) : IRequestHandler<GetCourse, IEnumerable<CourseDto>>
    {
        private readonly ICourseRepository courseRepository = courseRepository;

        public async Task<IEnumerable<CourseDto>> Handle(GetCourse request, CancellationToken cancellationToken)
        {
            var tagsMapper = new TagsMapper();
            var courseMapper = new CourseMapper();
            var courses = await courseRepository.GetAllCourses();

            if (courses == null)
            {
                throw new Exception("No courses found");
            }
            var coursesDtos = new List<CourseDto>();
            try
            {
                coursesDtos = courses.Select(course => courseMapper.CourseToCourseDto(course)).ToList();
                coursesDtos.ForEach(c => c.Author.Role = UserRoles.Lecturer);
                coursesDtos.ForEach(c => c.CoAuthors?.ForEach(ca => ca.Role = UserRoles.Lecturer));
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong, try again");
            }

            return coursesDtos;
        }
    }
}
