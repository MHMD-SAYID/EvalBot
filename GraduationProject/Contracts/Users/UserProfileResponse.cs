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
        public List<projectProfile> Projects { get; set; }
        public List<experienceProfile> Experience { get; set; }
        public List<educationProfile> Education { get; set; }
        public List<businessAccountProfile> Accounts { get; set; }
        public List<languageProfile> Languages { get; set; }

        public string Bio { get; set; }
        public string ProfilePicUrl { get; set; }
        public string CVUrl { get; set; }
    }
    public class projectProfile
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }
    public class businessAccountProfile
    {

        public int id { get; set; }

        public string AccountType { get; set; }
        public string AccountLink { get; set; }

       
    }

    public class educationProfile
    {

        public int id { get; set; }

        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public bool IsUnderGraduate { get; set; }
        //public long StartDate { get; set; }
        //public long EndDate { get; set; }

       
    }

    public class experienceProfile
    {

        public int id { get; set; }

        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public bool StillWorkingThere { get; set; }
        //public long StartDate { get; set; }
        //public long EndDate { get; set; }

     
    }public class languageProfile
    {

        public int id { get; set; }

        public string name { get; set; }
        public string lavel { get; set; }


    }

}
