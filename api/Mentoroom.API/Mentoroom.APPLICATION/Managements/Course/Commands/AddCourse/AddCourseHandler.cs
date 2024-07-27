using FluentValidation;
using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Validators;
using Mentoroom.DOMAIN.Entities.LecturerModels;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.AddCourse
{
    public class AddCourseHandler(
        ICourseRepository courseRepository,
        IDepartmentRepository departmentRepository,
        IMajorRepository majorRepository,
        IDegreeRepository degreeRepository,
        ISemesterRepository semesterRepository,
        IYearRepository yearRepository,
        ICourseCoAuthorRepository courseCoAuthorRepository,
        UserManager<AppUser> userManager,
        CourseValidator validator) : IRequestHandler<AddCourse, CourseDto>
    {
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<CourseDto> Handle(AddCourse request, CancellationToken cancellationToken)
        {
            var existingCourse = (await courseRepository.GetAllCourses()).FirstOrDefault(c => c.Name == request.Name);
            if (existingCourse != null)
            {
                throw new Exception("This course name already exists");
            }

            await validator.ValidateAndThrowAsync(request);
            var courseMapper = new CourseMapper();
            var userMapper = new AppUserMapper();

            var course = await courseRepository.AddCourse(new DOMAIN.Entities.Shared.Course()
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                IsActive = request.IsActive,
                Tags = new DOMAIN.Entities.Tags.CourseTags()
                {
                    Department = (await departmentRepository.GetDepartmentById(request.DepartmentId)).Name,
                    ShortDepartment = (await departmentRepository.GetDepartmentById(request.DepartmentId)).ShortName,
                    Major = (await majorRepository.GetMajorById(request.MajorId)).Name,
                    Degree = (await degreeRepository.GetDegreeById(request.DegreeId)).Name,
                    Year = (await yearRepository.GetYearById(request.YearId)).Name,
                    Semester = (await semesterRepository.GetSemesterById(request.SemesterId)).Name,
                },
                AuthorId = request.AuthorId,
                Author = await userManager.Users.FirstAsync(u => u.Id == request.AuthorId),
            });

            List<CourseCoAuthor> coAuthors = new List<CourseCoAuthor>();
            try
            {
                foreach (var coAuthorId in request.CoAuthorsId)
                {
                    coAuthors.Add(await courseCoAuthorRepository.AddCourseCoAuthor(coAuthorId, course.Id));
                }
                course.CoAuthors = coAuthors;
            }
            catch (Exception)
            {
                throw new Exception("Wrong coAuthors");
            }

            var createdCourse = await courseRepository.GetCourseById(course.Id);
            CourseDto courseDto = courseMapper.CourseToCourseDto(createdCourse);
            courseDto.Author.Role = UserRoles.Lecturer;
            courseDto.CoAuthors?.ForEach(ca => ca.Role = UserRoles.Lecturer);

            return courseDto;
        }
    }
}
