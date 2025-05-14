namespace GraduationProject.Errors;

    public static class PorfileErrors
    {
        public static readonly Error ProjectNotFound =
            new("Project.NotFound", "The specified project could not be found.", StatusCodes.Status404NotFound);
        public static readonly Error ExperienceNotFound =
            new("Experience.NotFound", "The specified experience could not be found.", StatusCodes.Status404NotFound);
        public static readonly Error EducationNotFound =
            new("Education.NotFound", "The specified education could not be found.", StatusCodes.Status404NotFound);
        public static readonly Error AccountNotFound =
            new("Account.NotFound", "The specified account could not be found.", StatusCodes.Status404NotFound);
        public static readonly Error LanguageNotFound =
            new("Lanugage.NotFound", "The specified Language could not be found.", StatusCodes.Status404NotFound);

        public static readonly Error InvalidSkillsUpdate =
            new("Skills.InvalidUpdate", "The provided list of skills is invalid.", StatusCodes.Status400BadRequest);
        public static readonly Error InvalidLangiageUpdate =
            new("Language.InvalidUpdate", "The provided language is invalid.", StatusCodes.Status400BadRequest);
    public static readonly Error InvalidLangiageAdd =
            new("Language.InvalidAdd", "The provided language is invalid.", StatusCodes.Status400BadRequest);
    public static readonly Error JobNotFound =
            new("Job.NotFound", "The specified Job could not be found.", StatusCodes.Status404NotFound);
}

