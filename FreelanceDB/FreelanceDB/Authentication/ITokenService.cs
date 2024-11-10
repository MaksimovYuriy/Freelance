using FreelanceDB.Database.Entities;
using System.Security.Claims;

namespace FreelanceDB.Authentication
{
    public interface ITokenService
    {
        string GenerateAccessToken(long id, string role);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        DateTime GetRefreshTokenExpireTime();
    }
}
