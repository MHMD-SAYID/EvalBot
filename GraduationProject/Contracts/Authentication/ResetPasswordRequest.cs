namespace GraduationProject.Contracts.Authentication;

public record ResetPasswordRequest
(
    string Email,
    string NewPassword
);
