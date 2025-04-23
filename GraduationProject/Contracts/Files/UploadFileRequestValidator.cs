namespace GraduationProject.Contracts.Files;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
        //RuleFor(x => x.File)
        //    .SetValidator(new FileSizeValidator())
        //    .SetValidator(new BlockedSignaturesValidator())
        //    .SetValidator(new FileNameValidator());
        //RuleFor(x => x.File)
        //    .Must((request, context) =>
        //    {
        //        var extension = Path.GetExtension(request.File.FileName.ToLower());
        //        return FileSettings.AllowedFileExtensions.Contains(extension);
        //    })
        //    .WithMessage("File extension is not allowed")
        //    .When(x => x.File is not null);



        RuleFor(x => x.File)
    .Must((request, context) =>
    {
        var extension = Path.GetExtension(request.File.FileName.ToLower());
        Console.WriteLine($"File extension: {extension}"); // Log extension for debugging
        return FileSettings.AllowedFileExtensions.Contains(extension);
    })
    .WithMessage("File extension is not allowed")
    .When(x => x.File is not null);

    }
}