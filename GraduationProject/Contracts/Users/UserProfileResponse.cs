namespace GraduationProject.Contracts.Users
{
    //public class UserProfileResponse
    //(
    //    string Email,
    //    string UserName,
    //    List<string> Skills,
    //    List<Authentication.Project> Projects,
    //    //List<Authentication.Experience> Experiences,
    //    //List<Authentication.Education> Educations,
    //    //List<Authentication.BusinessAccount> Accounts,
    //    string FirstLanguage,
    //    string FirstLanguageLevel,
    //    string SecondLanguage,
    //    string SecondLanguageLevel,
    //    string Bio,
    //    string ProfilePicUrl

    //);
    public class UserProfileResponse
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Skills { get; set; }
        public List<project> Projects { get; set; }
        public List<experience> Experience { get; set; }
        public List<education> Education { get; set; }
        public List<businessAccount> Accounts { get; set; }
        public string FirstLanguage { get; set; }
        public string FirstLanguageLevel { get; set; }
        public string SecondLanguage { get; set; }
        public string SecondLanguageLevel { get; set; }
        public string Bio { get; set; }
        public string ProfilePicUrl { get; set; }
        public string CVUrl { get; set; }
    }
    public class project
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }
    public class businessAccount
    {

        public int id { get; set; }

        public string AccountType { get; set; }
        public string AccountLink { get; set; }

       
    }

    public class education
    {

        public int id { get; set; }

        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public bool IsUnderGraduate { get; set; }
        //public long StartDate { get; set; }
        //public long EndDate { get; set; }

       
    }

    public class experience
    {

        public int id { get; set; }

        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public bool StillWorkingThere { get; set; }
        //public long StartDate { get; set; }
        //public long EndDate { get; set; }

     
    }

}
