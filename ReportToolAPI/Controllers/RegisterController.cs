using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReportToolAPI.Models;

namespace ReportToolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IValidator<Register> _validator;

    public RegisterController(UserManager<IdentityUser> userManager, IValidator<Register> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> Register(Register model)
    {
        var validation = _validator.Validate(model);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }
        if (await _userManager.FindByNameAsync(model.Email) != null)
            return BadRequest("User with this email already exists");

        var user = new IdentityUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest();

        return Ok();
    }
}