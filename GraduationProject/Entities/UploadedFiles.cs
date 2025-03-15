namespace GraduationProject.Entities
{
    public class UploadedFiles 
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public string UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
    }
}
