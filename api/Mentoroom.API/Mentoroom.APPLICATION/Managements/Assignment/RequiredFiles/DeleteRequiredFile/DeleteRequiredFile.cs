using MediatR;

namespace Mentoroom.APPLICATION.Managements.Assignment.RequiredFiles.DeleteRequiredFile
{
    public class DeleteRequiredFile : IRequest
    {
        public Guid RequiredFileId { get; set; }
    }
}
