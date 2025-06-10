namespace GraduationProject.Contracts.Files;

public record UploadFileRequest(
    IFormFile File,
    string userId
);
