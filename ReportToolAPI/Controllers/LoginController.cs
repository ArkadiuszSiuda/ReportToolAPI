using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReportToolAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReportToolAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IValidator<Login> _validator;

    public LoginController(UserManager<IdentityUser> userManager, IConfiguration configuration, IValidator<Login> validator)
    {
        _userManager = userManager;
        _configuration = configuration;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login loginModel)
    {
        var validation = _validator.Validate(loginModel);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors);
        }
        var user = await _userManager.FindByNameAsync(loginModel.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

            var token = GetToken(authClaims);

            return Ok(new JwtResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}