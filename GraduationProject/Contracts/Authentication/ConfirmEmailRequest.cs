﻿namespace GraduationProject.Contracts.Authentication;

public record ConfirmEmailRequest(
    string UserId,
    string Code
);