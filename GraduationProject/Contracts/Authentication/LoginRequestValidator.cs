﻿using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Contracts.Authentication
{
    public class LoginRequestValidator: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
