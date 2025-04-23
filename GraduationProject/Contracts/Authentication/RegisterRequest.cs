using AutoMapper;

namespace GraduationProject.Contracts.Authentication;

public record RegisterRequest(
    string Email,
    string Password,
    string UserName,
    string CountryOfResidence,
    string EmailType,
    string PhoneNumber,
    int gender,
    List<string> Skills,
    string YearsOfExperience,
    double ExpectedSalary,
    string Nationality,
    DateOnly DateOfBirth,
    string FirstLanguage,
    string FirstLanguageLevel,
    string SecondLanguage,
    string SecondLanguageLevel,
    List<Project> Projects,
    List<Experience> Experiences,
    List<Education> Educations,
    List<BusinessAccount> Accounts

);
public record BusinessAccount
(

    string AccountType,
    string AccountLink
);
public record Education
(
    string Institution,
    string Degree,
    string FieldOfStudy,
    bool IsUnderGraduate,
    long   StartDate,
    long   EndDate

);
public record Experience
(
    string JobTitle,
    string CompanyName,
    bool StillWorkingThere,
    long StartDate,
    long EndDate
);
public record Project
(
    string name,
    string link
);
