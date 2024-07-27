using MediatR;
using Mentoroom.DOMAIN.Entities.Shared;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Mentoroom.DOMAIN.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Mentoroom.APPLICATION.Managements.StudentManagment.StudentFiles.Commands.Delete
{
    public class DeleteStudentFileHandler(
        IStudentAssignmentFileRepository studentAssignmentFileRepository,
        IFileRepository fileRepository,
        IAssignmentRepository assignmentRepository,
        ICourseRepository courseRepository,
        UserManager<AppUser> userManager,
        IAssignmentFileRepository assignmentFileRepository,
        IStudentAssignmentRepository studentAssignmentRepository) : IRequestHandler<DeleteStudentFile>
    {
        private readonly IStudentAssignmentFileRepository studentAssignmentFileRepository = studentAssignmentFileRepository;
        private readonly IFileRepository fileRepository = fileRepository;

        public async Task Handle(DeleteStudentFile request, CancellationToken cancellationToken)
        {
            var assignmentFile = (await assignmentFileRepository.GetAllAssignmentFiles()).FirstOrDefault(af => af.Id == request.AssignmentFileId);
            if (assignmentFile == null)
            {
                throw new Exception("Wrong assignment file id");
            }

            var assignment = (await assignmentRepository.GetAssignmentById(assignmentFile.AssignmentId));
            var course = (await courseRepository.GetCourseById(assignment.CourseId));
            var student = await userManager.FindByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new Exception("This student does not exist");
            }
            if (assignment == null)
            {
                throw new Exception("This assignment does not exist");
            }
            else if (course == null)
            {
                throw new Exception("This course does not exist");
            }


            var studentAssignments = await studentAssignmentRepository.GetAllStudentAssignments();
            var studentAssignment = studentAssignments.FirstOrDefault(sa => sa.StudentCourse.StudentId == student.Id && sa.StudentCourse.CourseId == course.Id && sa.CourseAssignmentId == assignment.Id);

            if (studentAssignment == null)
            {
                throw new Exception("This student does not belong to this assignment");
            }

            var studentFile = (await studentAssignmentFileRepository.GetAllStudentAssignments()).FirstOrDefault(x => x.AssignmentFileId == request.AssignmentFileId && x.StudentAssignmentId == studentAssignment.Id);
            if (studentFile == null)
            {
                throw new Exception("There is no file with this id");
            }

            if (studentFile.Uri == null || studentFile.IsSended == false)
            {
                throw new Exception("This file is not sended, there is nothing to delete");
            }
            try
            {
                await fileRepository.DeleteByUri(studentFile.Uri);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }

            await studentAssignmentFileRepository.DeleteFile(studentFile.Id);
        }
    }
}
