using GraduationProject.DTO;
using GraduationProject.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        
       
        public AccountController(UserManager<ApplicationUser> UserManager)
        {
            userManager = UserManager;
            

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserFromRequest.UserName;
                user.Email = UserFromRequest.Email;
                IdentityResult result =
                    await userManager.CreateAsync(user, UserFromRequest.Password);
                if (result.Succeeded)
                {
                    return Created();
                }


                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("password", item.Description);
                }

            }

            return BadRequest(ModelState);
        }
       
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDTO UserFromRequest)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser UserFromDb = await userManager.FindByEmailAsync(UserFromRequest.Email);
                bool found =
                    await userManager.CheckPasswordAsync(UserFromDb, UserFromRequest.Password);
                if (found)
                {
                    List<Claim> UserClaims = new List<Claim>();

                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDb.Id));
                    UserClaims.Add(new Claim(ClaimTypes.Name, UserFromDb.Email));
                    var userRoles = await userManager.GetRolesAsync(UserFromDb);
                    foreach (var role in userRoles)
                    {
                        UserClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var SignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ksdlkjljskj2325#!#!vnl1jk2!#!@3213!#kjvljicojckl"));
                    SigningCredentials UserCred = new SigningCredentials(SignInKey, SecurityAlgorithms.HmacSha256);
                    JwtSecurityToken mytoken = new JwtSecurityToken
                        (
                            issuer: "http://localhost:5203",
                            audience: "http://localhost:54200",
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: UserCred


                        );

                    return Ok
                    (
                        new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expires = DateTime.Now.AddHours(1)
                        }
                    );

                }
                ModelState.AddModelError("Email", "Email or password invalid");
            }

            return BadRequest(ModelState);


        }
    }

}
