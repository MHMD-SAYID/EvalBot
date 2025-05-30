using GraduationProject.Contracts.Users;
using GraduationProject.Contracts.Users.Add;
using GraduationProject.Contracts.Users.Delete;
using GraduationProject.Contracts.Users.Interview;
using GraduationProject.Contracts.Users.Update;
using GraduationProject.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace GraduationProject.Controllers;

[ApiController]

public class AccountController(IUserService userService) : ControllerBase
{

    private readonly IUserService _userService = userService;
    [HttpGet("Profile")]
    public async Task<IActionResult> Info()
    {
        var result = await _userService.GetProfileAsync(User.GetUserId());

        return Ok(result.Value);
    }

    [HttpPost("Update_Bio")]
    public async Task<IActionResult> UpdateBio(UpdateBioRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateBio(request, cancellationToken);
        return Ok();

    }
    [HttpPost("Add_Experience")]
    public async Task<IActionResult> AddExperience(AddExperienceRequest request, CancellationToken cancellationToken)
    {

        var result = await _userService.AddExperience(request, cancellationToken);
        return Ok();
    }

    [HttpPost("Add_Education")]
    public async Task<IActionResult> AddEducation(AddEducationRequest request, CancellationToken cancellationToken)
    {

        var result = await _userService.AddEducation(request, cancellationToken);
        return Ok();
    }

    [HttpPost("Add_Project")]
    public async Task<IActionResult> AddProject(AddProjectRequest request, CancellationToken cancellationToken)
    {

        var result = await _userService.AddProject(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpPost("Add_BusinessAccount")]
    public async Task<IActionResult> AddBusinessAccount(AddBusinessAccountRequest request, CancellationToken cancellationToken)
    {

        var result = await _userService.AddBusinessAcount(request, cancellationToken);
        return Ok();
    }
    [HttpDelete("Delete_Education")]
    public async Task<IActionResult> DeleteEducation(DeleteRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteEducation(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpDelete("Delete_Experience")]
    public async Task<IActionResult> DeleteExperience(DeleteRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteExperience(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpDelete("Delete_Project")]
    public async Task<IActionResult> DeleteProject(DeleteRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteProject(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpDelete("Delete_BusinessAccount")]
    public async Task<IActionResult> DeleteBuinessAcount(DeleteRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteBusinessAccountLink(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpDelete("Delete_account")]
    public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteAccount(User.GetUserId(), cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpPut("Update-Skills")]
    public async Task<IActionResult> UpdateSkills(UpdateSkillsRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateSkills(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpPut("Update-experience")]
    public async Task<IActionResult> UpdateExperience(UpdateExperienceRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateExperience(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("Update-education")]
    public async Task<IActionResult> UpdateEducation(UpdateEducationRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateEducation(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPut("Update-project")]
    public async Task<IActionResult> UpdateProject(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateProject(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("Update-business-account")]
    public async Task<IActionResult> UpdateBusinessAccount(UpdateBusinessAccountRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateBusinessAccount(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("Update-language")]
    public async Task<IActionResult> UpdateLanguage(UpdateLanguageRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateLanguage(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpDelete("Delete-language")]
    public async Task<IActionResult> DeleteLanguage(DeleteRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteLanguages(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpPost("Add-language")]
    public async Task<IActionResult> AddLanguage(AddLanguagesRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddLanguages(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpGet("get-all-jobs")]
    public async Task<IActionResult> GetAllJobs(CancellationToken cancellationToken)
    {
        var response = await _userService.GetAllJobs(cancellationToken);

        return Ok(response.Value);

    }
    [HttpPost("apply-to-job")]
    public async Task<IActionResult> ApllyToJob(ApplyToJobRequest request, CancellationToken cancellationToken)
    {

        var result = await _userService.ApplyToJob(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("Conduct-interview")]
    public async Task<IActionResult> ConductInterView(CoductInterviewRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.ConductInterView(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("Add-interview-data")]
    public async Task<IActionResult> AddInterViewData(AddInterviewDataRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddInterViewData(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("Add-interview-vision-data")]
    public async Task<IActionResult> AddInterViewVisionData(AddVisionResultRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddInterViewVisionData(request, cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
