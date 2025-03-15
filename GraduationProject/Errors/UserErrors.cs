using GraduationProject.Abstractions;

namespace GraduationProject.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials = 
        new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCode =
        new("User.InvalidCode", "Invalid code", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);
    public static readonly Error DuplicatedUserName =
        new("User.DuplicatedUserName", "Username already exists", StatusCodes.Status409Conflict);
    public static readonly Error EmailNotFound =
        new("User.EmailNotFound", "The provided email does not exist", StatusCodes.Status404NotFound);
    public static readonly Error NoFileUploaded =
        new("CV.NoFileUploaded", "No File Uploaded", StatusCodes.Status404NotFound);

    public static readonly Error NotPDF =
        new("CV.NotPDF", "Wrong file type", StatusCodes.Status415UnsupportedMediaType);
    public static readonly Error UploadedSuccessfully =
       new("CV.UploadedSuccessfully", "Uploaded Successfully", StatusCodes.Status200OK);
    public static readonly Error InternalServerError =
       new("CV.InternalServerError", " Internal Server Error", StatusCodes.Status500InternalServerError);
}