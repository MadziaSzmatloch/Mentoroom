namespace Mentoroom.APPLICATION.Models
{
    public class AssignmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool IsActive { get; set; } = true;

        public string CourseName { get; set; }
        public List<AttachmentDto> AssignmentFiles { get; set; }
        public List<FileModel> RequiredFiles { get; set; }
    }
}
