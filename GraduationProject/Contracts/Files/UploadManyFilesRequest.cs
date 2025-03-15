namespace GraduationProject.Contracts.Files;

public record UploadManyFilesRequest(
    IFormFileCollection Files
);