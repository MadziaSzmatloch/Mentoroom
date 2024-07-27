namespace Mentoroom.APPLICATION.Models
{
    public class StudentFileDto
    {
        public Guid Id { get; set; }
        public string Extension { get; set; }
        public int MaxSizeInKb { get; set; }
        public string FileNameSuffix { get; set; }
        public bool IsSended { get; set; }
    }
}
