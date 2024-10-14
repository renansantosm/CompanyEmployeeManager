using CompanyEmployeeManager.DTOs.Models.Authetication;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CompanyEmployeeManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService, 
                          UserManager<ApplicationUser> userManager, 
                          RoleManager<IdentityRole> roleManager, 
                          IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var roleExist = await _roleManager.RoleExistsAsync(roleName);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK,
                     new ResponseDTO { Status = "Success", Message = $"Role {roleName} added successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                     new ResponseDTO { Status = "Success", Message = $"Role {roleName} added successfully" });
            }
        }
        return StatusCode(StatusCodes.Status400BadRequest,
            new ResponseDTO { Status = "Success", Message = $"Role already exists" });
    }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new ResponseDTO { Status = "Success", Message = $"User {user.Email} added to the {roleName} role" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseDTO { Status = "Error", Message = $"Error: Unable to add user {user.Email} to the {roleName} role" });
            }
        }
        return BadRequest(new { error = "Unable to find user" });
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
    {
        var user = await _userManager.FindByNameAsync(model.Username!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAcessToken(authClaims, _configuration);

            var refreshtoken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            user.RefreshToken = refreshtoken;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshtoken,
                Expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult> Register([FromBody] RegisterModelDTO model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "User already exist!" });
        }

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "User creation failed" });
        }

        return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult> RefreshToken(TokenModelDTO tokenModel)
    {
        if (tokenModel is null)
            return BadRequest("Invalid client request");

        string? acessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));

        string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));

        var principal = _tokenService.GetPrincipalFromExpiresToken(acessToken!, _configuration);

        if (principal == null)
            return BadRequest("Invalid acess token/refresh token");

        string username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid acess token/refresh token");

        var newAcessToken = _tokenService.GenerateAcessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAcessToken),
            RefreshToken = newRefreshToken,
        });
    }

    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<ActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return BadRequest("Invalid user name");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}
