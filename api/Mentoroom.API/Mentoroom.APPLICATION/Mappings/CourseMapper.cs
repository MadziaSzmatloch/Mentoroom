using Mentoroom.APPLICATION.Managements.Course;
using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Queries.GetCoursesFromStudent;
using Mentoroom.DOMAIN.Entities.Shared;
using Riok.Mapperly.Abstractions;

namespace Mentoroom.APPLICATION.Mappings
{
    [Mapper]
    public partial class CourseMapper
    {
        //[MapProperty(nameof(Course.CoAuthors), nameof(CourseDto.CoAuthors))]
        //public partial CourseDto CourseToCourseDto(Course course);
        public CourseDto CourseToCourseDto(Course course)
        {
            var tagsMapper = new TagsMapper();
            var userMapper = new AppUserMapper();


            var CoAuthors = course.CoAuthors.Select(x => x.CoAuthor).Select(y => userMapper.AppUserToUserDto(y)).ToList();


            var courseDto = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                IsActive = course.IsActive,
                Degree = course.Tags.Degree,
                Year = course.Tags.Year,
                Semester = course.Tags.Semester,
                Department = course.Tags.Department,
                ShortDepartment = course.Tags.ShortDepartment,
                Major = course.Tags.Major,
                Author = userMapper.AppUserToUserDto(course.Author),
                CoAuthors = course.CoAuthors != null && course.CoAuthors.Count > 0
                            ? course.CoAuthors.Select(x => x.CoAuthor).Select(y => userMapper.AppUserToUserDto(y)).ToList()
                            : null
            };

            return courseDto;
        }



        public partial GetStudentCourses CourseDtoToGetStudentCourses(CourseDto courseDto);
    }
}
