namespace GraduationProject.Contracts.Company
{
    public record CompanyProfileResponse
    (
        
        string userName,
        string Location,
        string companyImageURL,
        List<jobsProfile> jobs
    );
   public record jobsProfile
   (
       int id,
       string title,
       DateOnly releaseDate
   );
}
