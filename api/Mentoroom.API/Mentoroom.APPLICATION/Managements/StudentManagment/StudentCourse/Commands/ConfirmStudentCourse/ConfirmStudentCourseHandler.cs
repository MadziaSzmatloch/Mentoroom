using MediatR;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentCourse.Commands.ConfirmStudentCourse
{
    public class ConfirmStudentCourseHandler(IStudentCourseRepository studentCourseRepository,
        ICourseRepository courseRepository,
        IStudentAssignmentRepository studentAssignmentRepository,
        IStudentAssignmentFileRepository studentAssignmentFileRepository,
        UserManager<AppUser> userManager) : IRequestHandler<ConfirmStudentCourse>
    {
        private readonly IStudentCourseRepository studentCourseRepository = studentCourseRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly IStudentAssignmentRepository studentAssignmentRepository = studentAssignmentRepository;
        private readonly IStudentAssignmentFileRepository studentAssignmentFileRepository = studentAssignmentFileRepository;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Handle(ConfirmStudentCourse request, CancellationToken cancellationToken)
        {
            var student = await userManager.FindByIdAsync(request.studentId);
            var course = await courseRepository.GetCourseById(request.courseId);
            var studentCourse = (await studentCourseRepository.GetAllStudentCourses()).FirstOrDefault(sc => sc.CourseId == request.courseId && sc.StudentId == request.studentId);
            if (student == null)
            {
                throw new Exception("This student does not exist");
            }
            else if (course == null)
            {
                throw new Exception("This course does not exist");
            }
            if (studentCourse == null)
            {
                throw new Exception("This student is not on this course");
            }
            else
            {
                if (studentCourse.IsConfirmed == null)  //waiting to be confirmed
                {
                    await studentCourseRepository.ConfirmStudent(request.studentId, request.courseId);
                    foreach (var assignment in course.Assignments)
                    {
                        await studentAssignmentRepository.AddStudentAssignmnent(new DOMAIN.Entities.StudentModels.StudentAssignment()
                        {
                            IsCompleted = false,
                            StudentCourseId = studentCourse.Id,
                            CourseAssignmentId = assignment.Id
                        });

                        foreach (var file in assignment.RequiredFiles)
                        {
                            await studentAssignmentFileRepository.AddStudentFile(new DOMAIN.Entities.StudentModels.StudentAssignmentFile()
                            {
                                IsSended = false,
                                StudentAssignmentId = (await studentAssignmentRepository.GetAllStudentAssignments())
                                        .FirstOrDefault(sa => sa.StudentCourseId == studentCourse.Id && sa.CourseAssignmentId == assignment.Id).Id,
                                AssignmentFileId = file.Id
                            });
                        }
                    }
                }
                else if (studentCourse.IsConfirmed == true)
                {
                    throw new Exception("This student is already in this course");
                }
                else
                {
                    throw new Exception("This student cannot signup for this course");
                }
            }
        }
    }
}
