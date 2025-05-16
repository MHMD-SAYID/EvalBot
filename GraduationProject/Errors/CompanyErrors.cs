namespace GraduationProject.Errors
{
    public class CompanyErrors
    {
        public static readonly Error JobNotFound =
            new("Job.NotFound", "The specified Job could not be found.", StatusCodes.Status404NotFound);
    }
}
