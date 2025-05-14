using GraduationProject.Contracts.Company;
using GraduationProject.Contracts.Users.Delete;
using GraduationProject.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CompanyAccountController(ICompanyService companyService) : ControllerBase
    {
        private readonly ICompanyService _companyService = companyService;

        [HttpGet("company-profile")]
        public async Task<IActionResult> Info()
        {
            var result = await _companyService.GetCompanyProfileAsync(User.GetUserId());

            return Ok(result.Value);
        }
        [HttpPost("add-job")]
        public async Task<IActionResult>AddJob(AddJopRequest request,CancellationToken cancellationToken)
        {
            var result = await _companyService.AddJob(request, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        } 
        [HttpPost("Delete-job")]
        public async Task<IActionResult>DeleteJob(DeleteRequest request,CancellationToken cancellationToken)
        {
            var result = await _companyService.DeleteJob(request, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }
    }
}
