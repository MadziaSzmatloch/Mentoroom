using MediatR;
using Mentoroom.APPLICATION.Models;
using Microsoft.AspNetCore.Http;

namespace Mentoroom.APPLICATION.Managements.Assignment.Files.AddAssignmentFile
{
    public class AddAssignmentFile : IRequest<AssignmentDto>
    {
        public IFormFile File { get; set; }
        public Guid AssignmentId { get; set; }
        public string FileName { get; set; }
    }
}
