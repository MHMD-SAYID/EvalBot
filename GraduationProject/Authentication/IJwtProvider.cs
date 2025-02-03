﻿using GraduationProject.Models;

namespace GraduationProject.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user);
    string? ValidateToken(string token);
}