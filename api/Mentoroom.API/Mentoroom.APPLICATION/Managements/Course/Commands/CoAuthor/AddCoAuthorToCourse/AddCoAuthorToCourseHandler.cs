using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.AddCoAuthorToCourse
{
    public class AddCoAuthorToCourseHandler(ICourseCoAuthorRepository courseCoAuthorRepository,
        ICourseRepository courseRepository,
        UserManager<AppUser> userManager) : IRequestHandler<AddCoAuthorToCourse, UserModel>
    {
        private readonly ICourseCoAuthorRepository courseCoAuthorRepository = courseCoAuthorRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<UserModel> Handle(AddCoAuthorToCourse request, CancellationToken cancellationToken)
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
            else if (courseCoAuthor != null || course.AuthorId == request.CoAuthorId)
            {
                throw new Exception("This lecturer already is an author of this course");
            }
            await courseCoAuthorRepository.AddCourseCoAuthor(request.CoAuthorId, request.CourseId);
            var mapper = new AppUserMapper();
            var authorModel = mapper.AppUserToUserDto(lecturer);
            return authorModel;
        }
    }
}
