namespace Mentoroom.APPLICATION.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }
        public string Extension { get; set; }
        public int MaxSizeInKb { get; set; }
        public string FileNameSuffix { get; set; }
    }
}
