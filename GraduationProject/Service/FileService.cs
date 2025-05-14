using Microsoft.AspNetCore.Http;

namespace GraduationProject.Services;

public class FileService(IWebHostEnvironment webHostEnvironment, AppDbContext context, UserManager<User> userManager) : IFileService
{
   

    private readonly string _localfilesPath = $"{webHostEnvironment.WebRootPath}/CV/";
    private readonly string _localimagesPath = $"{webHostEnvironment.WebRootPath}/Images/";
    private readonly UserManager<User> _userManager=userManager;
    private readonly AppDbContext _context = context;

    public async Task<UploadFileResponse> UploadAsync(IFormFile file,string userId, CancellationToken cancellationToken = default)
    {
        //remove the old file if exists
        //if ( _context.Files.Any(x => x.UserId == userId))
        //{
        //    var image = await _context.Files.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        //    System.IO.File.Delete(image.Link);
        //    _context.Remove(image);
        //}
        //var uploadedFile = await SaveFile(file, cancellationToken);
        //uploadedFile.UserId = userId;
        //uploadedFile.Link = Path.Combine(_imagesPath, uploadedFile.StoredFileName);

        //await _context.AddAsync(uploadedFile, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);

        //var path = Path.Combine(_imagesPath, uploadedFile.StoredFileName);
        //return path;
        var ex = Path.GetExtension(file.FileName);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        var path = Path.Combine(_localfilesPath, $"{user.UserName}{ex}");
        // delete the image if exists
        if (System.IO.File.Exists(path))
        {
            var usercv = await  _context.UserCV.FirstOrDefaultAsync(x => x.userProfileId == userId);
            if (usercv != null)
            {
                _context.Remove(usercv);
                await _context.SaveChangesAsync();
            }
            System.IO.File.Delete(path);
        }
        var cv = new UserCV
        {
            userProfileId = user.Id,
            RealPath = _localfilesPath+ user.UserName + ex,
            Extension = ex,
            HostedPath  = "http://evalbot.runasp.net//CV//" + user.UserName + ex


        };
        await _context.AddAsync(cv);
       await _context.SaveChangesAsync();
        //var cv=await _context.AddAsync(new =>UserCV


        //    { })
        
        using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);
        //user.cvURL = "http://evalbot.runasp.net//CV//" + user.UserName + ex;
        await _userManager.UpdateAsync(user);
        var response = new UploadFileResponse(cv.HostedPath); 
        return response;
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
    public async Task<UploadImageResponse> UploadImageAsync(IFormFile image,string userId, CancellationToken cancellationToken = default)
    {
        var ex = Path.GetExtension(image.FileName);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        var path = Path.Combine(_localimagesPath, $"{user.UserName}{ex}");
        // delete the image if exists
        if (System.IO.File.Exists(path))
        {
            var userImage = await _context.UserImage.FirstOrDefaultAsync(x => x.userId == user.Id);
            if (userImage != null)
            {
                _context.Remove(userImage);
                await _context.SaveChangesAsync();
            }
            System.IO.File.Delete(path);
        }
        var im = new UserImage
        {
            userId = user.Id,
            RealPath = _localimagesPath + user.UserName + ex,
            Extension = ex,
            HostedPath = "http://evalbot.runasp.net//Images//" + user.UserName + ex


        };
        await _context.AddAsync(im);
        await _context.SaveChangesAsync();
        //var cv=await _context.AddAsync(new =>UserCV


        //    { })

        using var stream = System.IO.File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);
        //user.cvURL = "http://evalbot.runasp.net//CV//" + user.UserName + ex;
        await _userManager.UpdateAsync(user);
        var response = new UploadImageResponse(im.HostedPath);
        return response;
        //if (_context.Files.Any(x => x.UserId == userId))
        //{
        //    var cv = await _context.Files.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        //    System.IO.File.Delete(cv.Link);
        //    _context.Remove(cv);
        //}
        //var uploadedFile = await SaveFile(image, cancellationToken);
        //uploadedFile.UserId = userId;
        //uploadedFile.Link = Path.Combine(_filesPath, uploadedFile.StoredFileName);

        //await _context.AddAsync(uploadedFile, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);

        //var path = Path.Combine(_filesPath, uploadedFile.StoredFileName);
        //return path;
    }

    public async Task<(byte[] fileContent, string contentType, string fileName)> DownloadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.Files.FindAsync(id);

        if (file is null)
            return ([], string.Empty, string.Empty);

        var path = Path.Combine(_localfilesPath, file.StoredFileName);

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

        var path = Path.Combine(_localfilesPath, file.StoredFileName);

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

        var path = Path.Combine(_localfilesPath,$"{ uploadedFile.FileName}");

        using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);

        return uploadedFile;
    }

    public async Task<UploadImageResponse> UploadCompanyImageAsync(IFormFile image, string CompanyId, CancellationToken cancellationToken = default)
    {
        //var ex = Path.GetExtension(image.FileName);

        //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == CompanyId);

        //var path = Path.Combine(_localimagesPath, $"{user.UserName}{ex}");
        //// delete the image if exists
        //if (System.IO.File.Exists(path))
        //{
        //    var userImage = await _context.UserImage.FirstOrDefaultAsync(x => x.companyProfileId == user.Id);
        //    if (userImage != null)
        //    {
        //        _context.Remove(userImage);
        //        await _context.SaveChangesAsync();
        //    }
        //    System.IO.File.Delete(path);
        //}
        //var im = new UserImage
        //{
        //    companyProfileId = user.Id,
        //    RealPath = _localimagesPath + user.UserName + ex,
        //    Extension = ex,
        //    HostedPath = "http://evalbot.runasp.net//Images//" + user.UserName + ex,
        //    userProfileId = null


        //};
        //await _context.AddAsync(im);
        //await _context.SaveChangesAsync();
        ////var cv=await _context.AddAsync(new =>UserCV


        ////    { })

        //using var stream = System.IO.File.Create(path);
        //await image.CopyToAsync(stream, cancellationToken);
        ////user.cvURL = "http://evalbot.runasp.net//CV//" + user.UserName + ex;
        //await _userManager.UpdateAsync(user);
        //var response = new UploadImageResponse(im.HostedPath);
        //return response;


        var ex = Path.GetExtension(image.FileName);

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == CompanyId);

        var path = Path.Combine(_localimagesPath, $"{user.UserName}{ex}");
        // delete the image if exists
        if (System.IO.File.Exists(path))
        {
            var userImage = await _context.UserImage.FirstOrDefaultAsync(x => x.userId == user.Id);
            if (userImage != null)
            {
                _context.Remove(userImage);
                await _context.SaveChangesAsync();
            }
            System.IO.File.Delete(path);
        }
        var im = new UserImage
        {
            userId = user.Id,
            RealPath = _localimagesPath + user.UserName + ex,
            Extension = ex,
            HostedPath = "http://evalbot.runasp.net//Images//" + user.UserName + ex


        };
        await _context.AddAsync(im);
        await _context.SaveChangesAsync();
        //var cv=await _context.AddAsync(new =>UserCV


        //    { })

        using var stream = System.IO.File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);
        //user.cvURL = "http://evalbot.runasp.net//CV//" + user.UserName + ex;
        await _userManager.UpdateAsync(user);
        var response = new UploadImageResponse(im.HostedPath);
        return response;


    }
}