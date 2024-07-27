using FluentValidation;
using MediatR;
using Mentoroom.APPLICATION.Mappings;
using Mentoroom.APPLICATION.Models;
using Mentoroom.APPLICATION.Validators;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Interfaces.Shared;
using Microsoft.AspNetCore.Http;

namespace Mentoroom.APPLICATION.Managements.Assignment.Files.AddAssignmentFile
{
    public class AddAssignmentFileHandler(IAssignmentAttachmentRepository assignmentAttachmentRepository,
        IFileRepository fileRepository,
        ICourseRepository courseRepository,
        IAssignmentRepository assignmentRepository,
        AssignmentAttachmentValidator validator) : IRequestHandler<AddAssignmentFile, AssignmentDto>
    {
        private readonly IAssignmentAttachmentRepository assignmentAttachmentRepository = assignmentAttachmentRepository;
        private readonly IFileRepository fileRepository = fileRepository;
        private readonly ICourseRepository courseRepository = courseRepository;
        private readonly IAssignmentRepository assignmentRepository = assignmentRepository;
        private readonly AssignmentAttachmentValidator validator = validator;

        public async Task<AssignmentDto> Handle(AddAssignmentFile request, CancellationToken cancellationToken)
        {
            var assignment = await assignmentRepository.GetAssignmentById(request.AssignmentId);
            if (assignment == null)
            {
                throw new Exception("This assignment does not exist");
            }
            var course = (await courseRepository.GetAllCourses()).FirstOrDefault(c => c.Id == assignment.CourseId);

            var contentType = request.File.ContentType;

            var extension = MimeTypes.GetMimeTypeExtensions(contentType).First();

            if (extension == "conf")
            {
                extension = "txt";
            }

            var newFileName = $"{Path.GetFileNameWithoutExtension(request.FileName)}.{extension}";

            var file = new FormFile(request.File.OpenReadStream(), 0, request.File.Length, request.File.Name, newFileName)
            {
                Headers = request.File.Headers,
                ContentType = request.File.ContentType,
            };

            var blob = await fileRepository.UploadFile(file, $"assignmentfiles/{course.Name}/{assignment.Name}/{request.FileName}");

            var attachment = new DOMAIN.Entities.Shared.AssignmentAttachment()
            {
                Uri = blob.Uri,
                //Name = $"{request.FileName}.{blob.ContentType.Split('/')[1]}",
                Name = $"{request.FileName}.{extension}",
                ContentType = blob.ContentType,
                AssignmentId = request.AssignmentId
            };

            await validator.ValidateAndThrowAsync(attachment);

            await assignmentAttachmentRepository.AddAttachment(attachment);

            assignment = await assignmentRepository.GetAssignmentById(request.AssignmentId);
            var assignmentMapper = new AssignmentMapper();
            var assignmentDto = assignmentMapper.AssignmentToAssignmentDto(assignment);
            return assignmentDto;
        }
    }
}
