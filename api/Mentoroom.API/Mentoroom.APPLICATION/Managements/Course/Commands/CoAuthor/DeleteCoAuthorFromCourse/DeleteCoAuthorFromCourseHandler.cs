using MediatR;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.CoAuthor.DeleteCoAuthorFromCourse
{
    public class DeleteCoAuthorFromCourseHandler(
        ICourseCoAuthorRepository courseCoAuthorRepository,
        ICourseRepository courseRepository,
        UserManager<AppUser> userManager) : IRequestHandler<DeleteCoAuthorFromCourse>
    {
        private readonly ICourseCoAuthorRepository courseCoAuthorRepository = courseCoAuthorRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Handle(DeleteCoAuthorFromCourse request, CancellationToken cancellationToken)
        {

            var course = await courseRepository.GetCourseById(request.CourseId);
            var lecturers = await userManager.GetUsersInRoleAsync(UserRoles.Lecturer);
            var lecturer = lecturers.FirstOrDefault(l => l.Id == request.CoAuthorId);
            var courseCoAuthor = (await courseCoAuthorRepository.GetCoAuthors()).FirstOrDefault(cca => cca.CourseId == request.CourseId && cca.CoAuthorId == request.CoAuthorId);
            if (course == null)
            {
                throw new Exception("This course does not exist");
            }
            else if (lecturer == null)
            {
                throw new Exception("This lecturer does not exist");
            }
            else if (course.AuthorId == request.CoAuthorId)
            {
                throw new Exception("You cannot delete this author");
            }
            else if (courseCoAuthor == null)
            {
                throw new Exception("This lecturer is not an coAuthor of this course");
            }
            await courseCoAuthorRepository.DeleteCourseCoAuthor(courseCoAuthor);
        }
    }
}
