using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetCoursesFromStudent
{
    public class GetCoursesFromStudentHandler(
        IStudentCourseRepository studentCourseRepository,
        ICourseRepository courseRepository,
        UserManager<AppUser> userManager) : IRequestHandler<GetCoursesFromStudent, List<GetStudentCourses>>
    {
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<List<GetStudentCourses>> Handle(GetCoursesFromStudent request, CancellationToken cancellationToken)
        {
            var student = await userManager.FindByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new Exception("This student does not exist");
            }

            var studentCourses = await studentCourseRepository.GetStudentCoursesByStudentId(request.StudentId);
            if (studentCourses.Count() == 0)
            {
                throw new Exception("This student does not participate in any course");
            }

            List<GetStudentCourses> courses = new List<GetStudentCourses>();
            var mapper = new CourseMapper();
            foreach (var studentCourse in studentCourses)
            {
                var course = await courseRepository.GetCourseById(studentCourse.CourseId);
                var courseDto = mapper.CourseToCourseDto(course);
                var getStudentCourse = mapper.CourseDtoToGetStudentCourses(courseDto);
                getStudentCourse.IsConfirmed = studentCourse.IsConfirmed;
                courses.Add(getStudentCourse);
            }
            courses.ForEach(c => c.Author.Role = UserRoles.Lecturer);
            courses.ForEach(c => c.CoAuthors?.ForEach(ca => ca.Role = UserRoles.Lecturer));
            return courses;
        }
    }
}
