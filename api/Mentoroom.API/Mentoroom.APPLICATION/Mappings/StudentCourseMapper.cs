using Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse;
using Mentoroom.DOMAIN.Entities.StudentModels;
using Riok.Mapperly.Abstractions;

namespace Mentoroom.APPLICATION.Mappings
{
    [Mapper]
    public partial class StudentCourseMapper
    {
        [MapProperty(nameof(StudentCourse.Course), nameof(StudentCourseDto.CourseDto))]
        public partial StudentCourseDto StudentCourseToStudentCourseDto(StudentCourse studentCourse);
    }
}
