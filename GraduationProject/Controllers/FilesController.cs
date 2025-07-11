﻿

using GraduationProject.IService;
namespace FileManager.Api.Controllers;
[Route("[controller]")]
[ApiController]

public class FilesController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadFileRequest request, CancellationToken cancellationToken)
    {
        var filePath = await _fileService.UploadAsync(request.File,request.userId, cancellationToken);

        return Ok(filePath);
    }

    [HttpPost("upload-many")]
    public async Task<IActionResult> UploadMany([FromForm] UploadManyFilesRequest request, CancellationToken cancellationToken)
    {
        var filesIds = await _fileService.UploadManyAsync(request.Files, cancellationToken);

        return Ok(filesIds);
    }

    [HttpPost(template: "upload-image")]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request, CancellationToken cancellationToken)
    {
        var imagePath=await _fileService.UploadImageAsync(request.Image,request.userId, cancellationToken);

        return Ok(imagePath);
    } 
    [HttpPost(template: "upload-company-image")]
    public async Task<IActionResult> UploadCompanyImage([FromForm] UploadImageRequest request, CancellationToken cancellationToken)
    {
        var imagePath=await _fileService.UploadCompanyImageAsync(request.Image,request.userId, cancellationToken);

        return Ok(imagePath);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var (fileContent, contentType, fileName) = await _fileService.DownloadAsync(id, cancellationToken);

        return fileContent is [] ? NotFound() : File(fileContent, contentType, fileName);
    }

    [HttpGet("stream/{id}")]
    public async Task<IActionResult> Stream([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var (stream, contentType, fileName) = await _fileService.StreamAsync(id, cancellationToken);

        return stream is null ? NotFound() : File(stream, contentType, fileName, enableRangeProcessing: true);
    }
    [HttpPost("upload-audio")]
    public async Task<IActionResult> UploadAudio([FromForm] UploadAudioRequest request, CancellationToken cancellationToken)
    {
        return await _fileService.UploadAudioAsync(request.Audio, cancellationToken)
            .ContinueWith(task => Ok(task.Result));
    }
}