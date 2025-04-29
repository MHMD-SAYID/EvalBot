using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Contracts.Authentication
{
    public record LoginRequest
    (
        string Email,
        string Password,
        string EmailType
    );
}
