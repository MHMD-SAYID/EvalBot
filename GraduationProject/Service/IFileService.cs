namespace GraduationProject.Services;

public interface IFileService
{
    Task<UploadFileResponse> UploadAsync(IFormFile file, string userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default);
    Task<UploadImageResponse> UploadImageAsync(IFormFile image,string userId, CancellationToken cancellationToken = default);
    Task<(byte[] fileContent, string contentType, string fileName)> DownloadAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(FileStream? stream, string contentType, string fileName)> StreamAsync(Guid id, CancellationToken cancellationToken = default);
}