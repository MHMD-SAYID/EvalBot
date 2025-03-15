namespace GraduationProject.Contracts.Files;

public record UploadImageRequest(
    IFormFile Image,
    string userId
);