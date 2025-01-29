using System.ComponentModel.DataAnnotations;

public class ForgetPasswordDTO
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? ClientUri { get; set; }
}

public class ResetPasswordDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}