using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.Course.Queries.GetCourseByTeacher
{
    public class GetCourseByTeacherHandler(ICourseRepository courseRepository, UserManager<AppUser> userManager) : IRequestHandler<GetCourseByTeacher, IEnumerable<CourseDto>>
    {
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<IEnumerable<CourseDto>> Handle(GetCourseByTeacher request, CancellationToken cancellationToken)
        {
            var lecturer = (await userManager.GetUsersInRoleAsync(UserRoles.Lecturer)).FirstOrDefault(l => l.Id == request.TeacherId);
            if (lecturer == null)
            {
                throw new Exception("This teacher does not exist");
            }
            var courses = await courseRepository.GetCoursesByTeacher(request.TeacherId);
            var courseMapper = new CourseMapper();

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
