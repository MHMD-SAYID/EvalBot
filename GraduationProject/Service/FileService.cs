using Microsoft.AspNetCore.Http;

namespace GraduationProject.Services;

public class FileService(IWebHostEnvironment webHostEnvironment, AppDbContext context) : IFileService
{
   

    private readonly string _filesPath = $"{webHostEnvironment.WebRootPath}/CV";
    private readonly string _imagesPath = $"{webHostEnvironment.WebRootPath}/Images";
    private readonly AppDbContext _context = context;

    public async Task<string> UploadAsync(IFormFile file,string userId, CancellationToken cancellationToken = default)
    {
//remove the old file if exists
        if ( _context.Files.Any(x => x.UserId == userId))
        {
            var cv = await _context.Files.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
            System.IO.File.Delete(cv.Link);
            _context.Remove(cv);
        }
        var uploadedFile = await SaveFile(file, cancellationToken);
        uploadedFile.UserId = userId;
        uploadedFile.Link = Path.Combine(_filesPath, uploadedFile.StoredFileName);
        
        await _context.AddAsync(uploadedFile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var path = Path.Combine(_filesPath, uploadedFile.StoredFileName);
        return path;
    }

    public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
    {
        List<Entities.UploadedFiles> uploadedFiles = [];

        foreach (var file in files)
        {
            var uploadedFile = await SaveFile(file, cancellationToken);
            uploadedFiles.Add(uploadedFile);
        }

        await _context.AddRangeAsync(uploadedFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadedFiles.Select(x => x.Id).ToList();
    }
    // Upload image for user or update it
    public async Task UploadImageAsync(IFormFile image,string userId, CancellationToken cancellationToken = default)
    {
        string UserName = await _context.Users.Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefaultAsync(cancellationToken);

        var path = Path.Combine(_imagesPath, UserName);
        // delete the image if exists
        if (System.IO.File.Exists(path))
        {
            
            System.IO.File.Delete(path);
        }
        using var stream = System.IO.File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);
    }

    public async Task<(byte[] fileContent, string contentType, string fileName)> DownloadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.Files.FindAsync(id);

        if (file is null)
            return ([], string.Empty, string.Empty);

        var path = Path.Combine(_filesPath, file.StoredFileName);

        MemoryStream memoryStream = new();
        using FileStream fileStream  = new(path, FileMode.Open);
        fileStream.CopyTo(memoryStream);

        memoryStream.Position = 0;

        return (memoryStream.ToArray(), file.ContentType, file.FileName);
    }

    public async Task<(FileStream? stream, string contentType, string fileName)> StreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.Files.FindAsync(id);

        if (file is null)
            return (null, string.Empty, string.Empty);

        var path = Path.Combine(_filesPath, file.StoredFileName);

        var fileStream = System.IO.File.OpenRead(path);

        return (fileStream, file.ContentType, file.FileName);
    }

    private async Task<Entities.UploadedFiles> SaveFile(IFormFile file, CancellationToken cancellationToken = default)
    {
        var randomFileName = Path.GetRandomFileName();

        var uploadedFile = new Entities.UploadedFiles
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            StoredFileName = file.FileName,
            FileExtension = Path.GetExtension(file.FileName)
        };

        var path = Path.Combine(_filesPath,$"{ uploadedFile.FileName}.{uploadedFile.FileExtension}");

        using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);

        return uploadedFile;
    }
}