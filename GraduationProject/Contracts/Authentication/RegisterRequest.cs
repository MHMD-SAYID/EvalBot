﻿using AutoMapper;

namespace GraduationProject.Contracts.Authentication;

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string UserName
);