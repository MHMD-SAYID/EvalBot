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
    string SecondLanguageLevel
    //projectRegister Projects,
    //experienceRegister Experiences,
    //educationList Educations,
    //businessAccountList Accounts

);
public record businessAccountList(List<businessAccountRegister> accounts);
public record educationList(List<educationRegister> education);
public record projectList(List<projectRegister> projects);
public record experienceList(List<experienceRegister> experience);
public record businessAccountRegister
(

    string AccountType,
    string AccountLink
);
public record educationRegister
(
    string Institution,
    string Degree,
    string FieldOfStudy,
    bool IsUnderGraduate,
    long StartDate,
    long EndDate

);
public record experienceRegister
(
    string JobTitle,
    string CompanyName,
    bool StillWorkingThere,
    long StartDate,
    long? EndDate
);
public record projectRegister
(
    string Name,
    string Link
);
