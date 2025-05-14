using GraduationProject.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace GraduationProject.Entities
{
    public partial class User:IdentityUser
    {
        public User()
        {
            
        }
        public List<RefreshToken> RefreshTokens { get; set; } = [];
        public UserImage Image { get; set; }
        public UserProfile? userProfile { get; set; }
        public CompanyProfile? companyProfile { get; set; }


        //Company 

        //public List<UserLanguage> userLanguages { get; set; }
        //public UploadedFiles uploadedFiles { get; set; }
        //public string? ImageURL { get; set; }
        //public string? cvURL { get; set; }
        //public string FirstLanguage { get; set; }
        //public string FirstLanguageLevel { get; set; }
        //public string SecondLanguage { get; set; }
        //public string SecondLanguageLevel { get; set; }
    }
}
