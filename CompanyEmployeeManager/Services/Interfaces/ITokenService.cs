using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CompanyEmployeeManager.Services.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAcessToken(IEnumerable<Claim> claims, IConfiguration _config);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiresToken(string token, IConfiguration _config);
}
