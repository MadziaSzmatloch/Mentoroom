using FluentValidation;
using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Validators;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.Tags;
using Mentoroom.DOMAIN.Models;

namespace Mentoroom.APPLICATION.Managements.Course.Commands.UpdateCourse
{
    public class UpdateCourseHandler(
        ICourseRepository courseRepository,
        IDepartmentRepository departmentRepository,
        IMajorRepository majorRepository,
        IDegreeRepository degreeRepository,
        ISemesterRepository semesterRepository,
        IYearRepository yearRepository,
        ICourseCoAuthorRepository courseCoAuthorRepository,
        CourseValidator validator) : IRequestHandler<UpdateCourse, CourseDto>
    {

        public async Task<CourseDto> Handle(UpdateCourse request, CancellationToken cancellationToken)
        {
            var existingCourse = await courseRepository.GetCourseById(request.Id);
            if (existingCourse == null)
            {
                throw new Exception($"This course does not exist");
            }

            await validator.ValidateAndThrowAsync(request);
            var tagsMapper = new TagsMapper();

            var course = new DOMAIN.Entities.Shared.Course()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
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
            };

            var coAuthors = await courseCoAuthorRepository.GetCoAuthorsByCourseId(request.Id);
            var coAuthorsIds = coAuthors.Select(cca => cca.CoAuthorId);
            var coAuthorsToDelete = coAuthorsIds.Except(request.CoAuthorsId);
            var coAuthorsToAdd = request.CoAuthorsId.Except(coAuthorsIds);

            await courseRepository.UpdateCourse(course);

            try
            {
                foreach (var author in coAuthorsToDelete)
                {
                    await courseCoAuthorRepository.DeleteCourseCoAuthor(new DOMAIN.Entities.LecturerModels.CourseCoAuthor() { CoAuthorId = author, CourseId = request.Id });
                }

                foreach (var author in coAuthorsToAdd)
                {
                    await courseCoAuthorRepository.AddCourseCoAuthor(author, request.Id);
                }
            }
            catch
            {
                throw new Exception("Wrong coAuthors");
            }


            var courseMapper = new CourseMapper();
            var newCourse = await courseRepository.GetCourseById(request.Id);
            CourseDto courseDto = courseMapper.CourseToCourseDto(newCourse);
            courseDto.Author.Role = UserRoles.Lecturer;
            courseDto.CoAuthors?.ForEach(ca => ca.Role = UserRoles.Lecturer);

            return courseDto;
        }
    }
}
