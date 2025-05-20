using CompanyEmployeeManager.DTOs.Models.Authetication;
using CompanyEmployeeManager.Models;
using CompanyEmployeeManager.Services.Interfaces;
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

    /// <summary>
    /// Creates a new role in the system.
    /// </summary>
    /// <param name="roleName">The name of the role to be created.</param>
    /// <returns>
    /// An HTTP status 200 if the role is successfully created, 
    /// or 400 if the role already exists or if an error occurs during creation.
    /// </returns>    
    [HttpPost]
    [Route("CreateRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
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

    /// <summary>
    /// Adds a user to a specified role.
    /// </summary>
    /// <param name="email">The email of the user to be added to the role.</param>
    /// <param name="roleName">The name of the role to which the user will be added.</param>
    /// <returns>
    /// An HTTP status 200 if the user is successfully added to the role, 
    /// 400 if the user is not found or if an error occurs during the operation.
    /// </returns>    
    [HttpPost]
    [Route("AddUserToRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
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

    /// <summary>
    /// Authenticates a user and generates an access token and a refresh token.
    /// </summary>
    /// <param name="model">A <c>LoginModelDTO</c> object containing the login credentials of the user.</param>
    /// <returns>
    /// An HTTP status 200 with the generated access token, refresh token, and expiration details 
    /// if authentication is successful, or status 401 if authentication fails.
    /// </returns>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
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
        return Unauthorized("Invalid username or password. Please check your credentials and try again.");
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="model">A <c>RegisterModelDTO</c> object containing the registration details of the user.</param>
    /// <returns>
    /// An HTTP status 200 with a success message if the user is created successfully, 
    /// or status 500 with an error message if the user already exists or if user creation fails.
    /// </returns>
    [HttpPost]
    [Route("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseDTO), StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
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

    /// <summary>
    /// Generates new access and refresh tokens based on a valid refresh token.
    /// </summary>
    /// <param name="tokenModel">
    /// A <c>TokenModelDTO</c> object containing the client's access token and refresh token.
    /// </param>
    /// <returns>
    /// A new pair of tokens (access and refresh) if the operation is successful, 
    /// or an HTTP status 400 if the token request is invalid or expired.
    /// </returns>
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

    /// <summary>
    /// Revokes a user's refresh token.
    /// </summary>
    /// <param name="username">The username whose refresh token will be revoked.</param>
    /// <returns>
    /// An HTTP status 204 (no content) if the revocation is successful, 
    /// or 400 if the user is not found.
    /// </returns>
    [HttpPost]
    [Route("revoke/{username}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null) return BadRequest("Invalid user name");

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}
